namespace MakerSpace.Models
{
    public class Favorites
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<FavoritePattern> FavoritePatterns { get; set; }
    }
}
