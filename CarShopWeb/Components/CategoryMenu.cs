using CarShopWeb.Model.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShopWeb.Components
{
    public class CategoryMake : ViewComponent
    {
        private readonly IMakeRepository _makeRepository;
        public CategoryMake(IMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _makeRepository.Make.OrderBy(p => p.MakeName);
            return View(categories);
        }
    }
}
