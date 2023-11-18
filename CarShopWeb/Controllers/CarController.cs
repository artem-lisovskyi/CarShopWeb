using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using CarShopWeb.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CarShopWeb.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMakeRepository _makeRepository;
        public CarController(ICarRepository carRepository, IMakeRepository makeRepository)
        {
            _carRepository = carRepository;
            _makeRepository = makeRepository;
        }


        public ViewResult List(string make)
        {
            string _make = make;
            IEnumerable<Car> cars;
            string currentMake = string.Empty;

            if (string.IsNullOrEmpty(make))
            {
                cars = _carRepository.Cars.OrderBy(p => p.CarId);
                currentMake = "All cars";
            }
            else
            {
                switch (_make.ToUpperInvariant())
                {
                    case "AUDI":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Audi".ToLower()));
                        break;

                    case "BMW":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("BMW".ToLower()));
                        break;

                    case "CHEVROLET":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Chevrolet".ToLower()));
                        break;
                    case "FORD":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Ford".ToLower()));
                        break;
                    case "HYUNDAI":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Hyundai".ToLower()));
                        break;
                    case "LEXUS":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Lexus".ToLower()));
                        break;
                    case "MAZDA":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Mazda".ToLower()));
                        break;
                    case "TESLA":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Tesla".ToLower()));
                        break;
                    case "NISSAN":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Nissan".ToLower()));
                        break;
                    case "TOYOTA":
                        cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains("Toyota".ToLower()));
                        break;
                    default:
                        throw new ArgumentException();
                }

                currentMake = _make;
            }

            return View(new CarListViewModel
            {
                Cars = cars,
                CurrentMake = currentMake
            });
        }
        public ViewResult Search(string searchString)
        {
            string _searchString = searchString;
            IEnumerable<Car> cars;
            string currentMake = string.Empty;

            if (string.IsNullOrEmpty(_searchString))
            {
                cars = _carRepository.Cars.OrderBy(p => p.CarId);
            }
            else
            {
                cars = _carRepository.Cars.Where(p => p.Make.MakeName.ToLower().Contains(_searchString.ToLower()));
            }

            return View("~/Views/Car/List.cshtml", new CarListViewModel { Cars = cars, CurrentMake = "All cars" });
        }

        public ViewResult Details(int carId)
        {
            var car = _carRepository.Cars.FirstOrDefault(d => d.CarId == carId);

            if (car == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(car);
        }

    }
}
