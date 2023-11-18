namespace CarShopWeb.Model.Data
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Car Car { get; set; }
        public virtual Order Order { get; set; }
    }
}
