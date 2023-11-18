using CarShopWeb.Model.data;
using CarShopWeb.Model.Data;
using CarShopWeb.Model.interfaces;
using CarShopWeb.Model.Repository;
using NLog.Fluent;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddTransient<IMakeRepository, DataMakeRepository>();
services.AddTransient<ICarRepository, DataCarRepository>();

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped(sp => ShoppingCart.GetCart(sp, "C:\\Users\\Artem\\source\\repos\\CarShopWeb\\CarShopWeb\\Model\\DataStore\\ShoppingCart.json"));
services.AddTransient<List<Order>>();
services.AddTransient<List<OrderDetail>>();
services.AddTransient<IOrderRepository, DataOrderRepository>();

services.AddMvc();
services.AddMemoryCache();
services.AddSession();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
                    name: "countryfilter",
                    pattern: "Car/{action}/{make?}",
                    defaults: new { Controller = "Car", action = "List" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{Id?}");
});

app.Run();
