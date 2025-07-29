namespace MakerSpace.Models
{
    public class Pattern
    {
        public int Id {  get; set; }
        public string Name { get; set; } = default!;
        public int MakerId { get; set; }
        public User Maker { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; } = default!;
        public DateOnly Published { get; set; }
        public decimal Price { get; set; }
        public string Photo1 { get; set; } = default!;
        public string? Photo2 { get; set; }
        public string? Photo3 { get; set;}
        public string Pdf { get; set; } = default!;
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }

        public List<FavoritePattern> FavoritePatterns { get; set; }
        public List<LibraryPattern> LibraryPatterns { get; set; }
        public List<PatternTag> PatternTags { get; set; }
        public User? User { get; set; }
    }
}
