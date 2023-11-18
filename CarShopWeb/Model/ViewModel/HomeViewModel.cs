using CarShopWeb.Model.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarShopWeb.Model.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Car> PreferredCars { get; set; }
    }
}
