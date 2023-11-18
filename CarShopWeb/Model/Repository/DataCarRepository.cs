using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CarShopWeb.Model.data
{
    public class DataCarRepository : ICarRepository
    {
        private readonly IMakeRepository _makeRepository = new DataMakeRepository();
        private const string FilePath = "C:\\Users\\Artem\\source\\repos\\CarShopWeb\\CarShopWeb\\Model\\DataStore\\Cars.json";

        private List<Car> _cars;

        public IEnumerable<Car> Cars
        {
            get
            {
                if (_cars == null)
                {
                    LoadCarsFromFile();
                }

                return _cars;
            }
        }

        public IEnumerable<Car> PreferredCars
        {
            get
            {
                return Cars.Where(c => c.IsPreferredCar).ToList();
            }
        }

        public Car GetCarById(int carId)
        {
            if (_cars == null)
            {
                LoadCarsFromFile();
            }

            return _cars.FirstOrDefault(c => c.CarId == carId);
        }

        private void LoadCarsFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    _cars = JsonConvert.DeserializeObject<List<Car>>(json);
                }
                else
                {
                    _cars = new List<Car>();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());

            }
        }
    }
}

