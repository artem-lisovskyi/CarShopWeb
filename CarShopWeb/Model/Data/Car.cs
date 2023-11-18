namespace CarShopWeb.Model.Data
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public decimal VolumeOfEngine { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsPreferredCar { get; set; }
        public int MakeId { get; set; }
        public virtual Make Make { get; set; }

    }
}
