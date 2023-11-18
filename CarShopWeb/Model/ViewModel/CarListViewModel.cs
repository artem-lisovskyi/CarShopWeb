using CarShopWeb.Model.Data;

namespace CarShopWeb.Model.ViewModel
{
    public class CarListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string CurrentMake { get; set; }
    }
}
