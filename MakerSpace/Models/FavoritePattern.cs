namespace MakerSpace.Models
{
    public class FavoritePattern
    {
        public int FavoritesId { get; set; }
        public int PatternId {  get; set; }
        public Pattern? Pattern { get; set; }
        public Favorites? Favorites { get; set; }
    }
}
