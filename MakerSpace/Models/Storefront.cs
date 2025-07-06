namespace MakerSpace.Models
{
    public class Storefront
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SellerId { get; set; }
        public User Seller { get; set; }
        public string Description { get; set; }
        public string BannerPhoto { get; set; }
    }
}
