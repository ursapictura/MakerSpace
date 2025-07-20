using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class PatternData
    {
        public static List<Pattern> Patterns = new()
        {
            new() {Id = 1, MakerId = 1, Name = "Kitchy Cooking Apron", CategoryId = 1, Description = "Not your grandma\'s kitchen apron, but it has the same vibe! Bake your favorite dishes while wearing this retro apron inspired by your coziest memories.", Published = new DateOnly(2025, 07, 1), Price = 12.00M, Photo1 = "example/path/fileName/1", Photo2 = "example/path/fileName/2", Photo3 = "example/path/fileName/3", Pdf = "example/PdfPath/1", IsApproved = true, IsDeleted = false },
            new() {Id = 2, MakerId = 2, Name = "Crochet Death Star", CategoryId = 2, Description = "That's no moon....It's a Death Star! Recreate your favorite planet destroyer with this fun and simple amigurumi crochet pattern.", Published = new DateOnly(2025, 07, 2), Price = 8.00M, Photo1 = "example/path/fileName/4", Photo2 = "example/path/fileName/5", Photo3 = "example/path/fileName/6", Pdf = "example/PdfPath/2", IsApproved = true, IsDeleted = false },
            new() {Id = 3, MakerId = 3, Name = "Sunday Tea Time Shawl ", CategoryId = 3, Description = "Snuggle up with a book and your favorite afternoon tea while wearing this cozy woll shawl.", Published = new DateOnly(2025, 07, 3), Price = 8.00M, Photo1 = "example/path/fileName/7", Photo2 = "example/path/fileName/8", Photo3 = "example/path/fileName/9", Pdf = "example/PdfPath/3", IsApproved = true, IsDeleted = false },
            new() {Id = 4, MakerId = 4, Name = "Octopus Quilt Block", CategoryId = 4, Description = "A nautical inspired quilt block.", Published = new DateOnly(2025, 07, 4), Price = 5.00M, Photo1 = "example/path/fileName/10", Photo2 = "example/path/fileName/11", Photo3 = "example/path/fileName/12", Pdf = "example/PdfPath/4", IsApproved = true, IsDeleted = false },
            new() {Id = 5, MakerId = 5, Name = "Fairy Garden Embroidery", CategoryId = 5, Description = "Create your own custome fairy garden embroidery with flowers, mushrooms, and a tiny fairy cottage! This pattern is suitable for 8 inch hoops.", Published = new DateOnly(2025, 07, 5), Price = 13.00M, Photo1 = "example/path/fileName/113", Photo2 = "example/path/fileName/14", Photo3 = "example/path/fileName/15", Pdf = "example/PdfPath/5", IsApproved = true, IsDeleted = false },
        };
    }
}
