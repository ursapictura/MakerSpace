using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class FavoritesData
    {
        public static List<Favorites> Favorites = new()
        {
           new () { Id = 1,
           UserId = 1 },

           new() { Id = 2,
           UserId = 2 },

           new() { Id = 3,
           UserId = 3 },

           new() { Id = 4,
           UserId = 4 },

           new() { Id = 5,
           UserId = 5 },
        };
    }
}
