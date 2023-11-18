using CarShopWeb.Model.interfaces;
using CarShopWeb.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CarShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        public HomeController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PreferredCars = _carRepository.PreferredCars
            };
            return View(homeViewModel);
        }
    }
}
