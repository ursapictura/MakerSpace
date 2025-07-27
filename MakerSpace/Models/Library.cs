namespace MakerSpace.Models
{
    public class Library
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<LibraryPattern> LibraryPatterns { get; set; }
        public User? User { get; set; }
    }
}
