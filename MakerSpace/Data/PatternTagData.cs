using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class PatternTagData
    {
        public static List<PatternTag> PatternTags = new()
        {
            new() { PatternId = 1, TagId = 1 },
            new() { PatternId = 1, TagId = 2 },
            new() { PatternId = 2, TagId = 2 },
            new() { PatternId = 2, TagId = 4 },
            new() { PatternId = 3, TagId = 1 },
            new() { PatternId = 4, TagId = 3 },
            new() { PatternId = 4, TagId = 5 },
            new() { PatternId = 5, TagId = 6 },
        };
    }
}
