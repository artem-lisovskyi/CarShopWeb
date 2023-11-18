using CarShopWeb.Model.Data;

namespace CarShopWeb.Model.interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
