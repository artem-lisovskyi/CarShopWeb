using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using System;

namespace CarShopWeb.Model.Repository
{
    public class DataOrderRepository : IOrderRepository
    {
        private ShoppingCart _shoppingCart;
        private List<Order> _orders;
        private List<OrderDetail> _ordersDetail;
        public DataOrderRepository(List<Order> orders, List<OrderDetail> ordersDetail, ShoppingCart shoppingCart)
        {
            _orders = orders;
            _ordersDetail = ordersDetail;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _orders.Add(order);
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    CarId = shoppingCartItem.Car.CarId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Car.Price
                };

                _ordersDetail.Add(orderDetail);
            }
        }
    }
}
