using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using Newtonsoft.Json;

namespace CarShopWeb.Model.data
{
    public class DataMakeRepository : IMakeRepository
    {
        private const string FilePath = "C:\\Users\\Artem\\source\\repos\\CarShopWeb\\CarShopWeb\\Model\\DataStore\\Make.json";
        private List<Make> _makes;

        public IEnumerable<Make> Make
        {
            get
            {
                if (_makes == null)
                {
                    LoadMakesFromFile();
                }

                return _makes;
            }
        }

        private void LoadMakesFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    _makes = JsonConvert.DeserializeObject<List<Make>>(json);
                }
                else
                {
                    _makes = new List<Make>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

