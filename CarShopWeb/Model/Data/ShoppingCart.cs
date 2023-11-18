using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace CarShopWeb.Model.Data
{
    public class ShoppingCart
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _jsonFilePath;

        private ShoppingCart(IHttpContextAccessor httpContextAccessor, string jsonFilePath)
        {
            _httpContextAccessor = httpContextAccessor;
            _jsonFilePath = jsonFilePath;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services, string jsonFilePath)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(
                services.GetRequiredService<IHttpContextAccessor>(),
                jsonFilePath
            )
            {
                ShoppingCartId = cartId,
                ShoppingCartItems = LoadShoppingCartItems(jsonFilePath, cartId)
            };
        }

        public void AddToCart(Car car, int amount)
        {
            var shoppingCartItem = ShoppingCartItems.SingleOrDefault(
                s => s.Car.CarId == car.CarId && s.ShoppingCartId == ShoppingCartId
            );

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Car = car,
                    Amount = 1
                };

                ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            SaveChanges();
        }

        public int RemoveFromCart(Car car)
        {
            var shoppingCartItem = ShoppingCartItems.SingleOrDefault(
                s => s.Car.CarId == car.CarId && s.ShoppingCartId == ShoppingCartId
            );

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems;
        }

        public void ClearCart()
        {
            ShoppingCartItems.Clear();
            SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            return ShoppingCartItems
                .Sum(c => c.Car.Price * c.Amount);
        }

        private void SaveChanges()
        {
            SaveShoppingCartItems(_jsonFilePath, ShoppingCartId, ShoppingCartItems);
        }

        private static List<ShoppingCartItem> LoadShoppingCartItems(string jsonFilePath, string cartId)
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    var json = File.ReadAllText(jsonFilePath);
                    var cartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(json);
                    return cartItems.Where(c => c.ShoppingCartId == cartId).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }

            return new List<ShoppingCartItem>();
        }

        private static void SaveShoppingCartItems(string jsonFilePath, string cartId, List<ShoppingCartItem> cartItems)
        {
            try
            {
                var existingCartItems = LoadShoppingCartItems(jsonFilePath, cartId);
                existingCartItems.RemoveAll(c => c.ShoppingCartId == cartId);
                existingCartItems.AddRange(cartItems);

                var json = JsonConvert.SerializeObject(existingCartItems, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
    }


}
