using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class CategoryData
    {
        public static List<Category> Categories = new()
        {
            new() {Id = 1, Name = "Sewing"},
            new() {Id = 2, Name = "Amigurumi"},
            new() {Id = 3, Name = "Knit"},
            new() {Id = 4, Name = "Quilting"},
            new() {Id = 5, Name = "Embroidery"},
            new() {Id = 6, Name = "Sashiko"},
            new() {Id = 7, Name = "Crochet"},
            new() {Id = 8, Name = "Weaving"}
        };
    }
}
