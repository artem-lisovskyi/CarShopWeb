using CarShopWeb.Model.Data;

namespace CarShopWeb.Model.interfaces
{
    public interface IMakeRepository
    {
        IEnumerable<Make> Make { get; }
    }
}
