using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using CarShopWeb.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarShopWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICarRepository _carRepository;
        private ShoppingCart _shoppingCart;

        public ShoppingCartController(ICarRepository carRepository, ShoppingCart shoppingCart)
        {
            _carRepository = carRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int carId)
        {
            var selectedCar = _carRepository.Cars.FirstOrDefault(p => p.CarId == carId);
            if (selectedCar != null)
            {
                _shoppingCart.AddToCart(selectedCar, 1);
            }

            return RedirectToAction("Index");
            
        }

        public RedirectToActionResult RemoveFromShoppingCart(int carId)
        {
            var selectedCar = _carRepository.Cars.FirstOrDefault(p => p.CarId == carId);
            if (selectedCar != null)
            {
                _shoppingCart.RemoveFromCart(selectedCar);
            }
            return RedirectToAction("Index");
        }
    }
}