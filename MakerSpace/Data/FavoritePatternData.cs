using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class FavoritePatternData
    {
        public static List<FavoritePattern> FavoritePatterns = new()
        {
           new() { FavoritesId = 1,
               PatternId = 1 },

           new() { FavoritesId = 2,
               PatternId = 2 },

           new() { FavoritesId = 3,
               PatternId = 3 },

           new() { FavoritesId = 4,
               PatternId = 4 },

           new() { FavoritesId = 5,
               PatternId = 5 },
        };
    }
}
