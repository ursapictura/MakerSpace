using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class TagData
    {
        public static List<Tag> Tags = new()
        {
            new() {Id = 1, Name = "Clothing"},
            new() {Id = 2, Name = "Toys"},
            new() {Id = 3, Name = "Nautical"},
            new() {Id = 4, Name = "SciFi"},
            new() {Id = 5, Name = "Animals"},
            new() {Id = 6, Name = "Fantasy"},
            new() {Id = 7, Name = "Easy"}
        };
    }
}
