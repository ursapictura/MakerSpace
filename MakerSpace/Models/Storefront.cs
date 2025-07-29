namespace MakerSpace.Models
{
    public class Storefront
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public string Description { get; set; } = default!;
        public string? BannerPhoto { get; set; }
    }
}
