using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class LibraryData
    {
        public static List<Library> Libraries = new()
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
