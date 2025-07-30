namespace MakerSpace.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<PatternTag> PatternTags { get; set; }
    }
}
