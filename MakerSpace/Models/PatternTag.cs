namespace MakerSpace.Models
{
    public class PatternTag
    {
        public int TagId { get; set; }
        public int PatternId { get; set; }
        public Tag? Tag { get; set; }
        public Pattern? Pattern { get; set; }
    }
}
