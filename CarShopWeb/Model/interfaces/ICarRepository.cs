using CarShopWeb.Model.Data;

namespace CarShopWeb.Model.interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get; }
        IEnumerable<Car> PreferredCars { get; }
        Car GetCarById(int carId);
    }
}
