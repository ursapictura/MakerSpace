namespace MakerSpace.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Pattern> Patterns { get; set; }
    }
}
