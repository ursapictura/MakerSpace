namespace MakerSpace.Models
{
    public class LibraryPattern
    {
        public int LibraryId { get; set; }
        public int PatternId { get; set; }
        public Library? Library { get; set; }
        public Pattern? Pattern { get; set; }
    }
}
