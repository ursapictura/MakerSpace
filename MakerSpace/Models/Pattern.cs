namespace MakerSpace.Models
{
    public class Pattern
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string MakerId { get; set; }
        public User Maker { get; set; }
        public string Catergory { get; set; }
        public string Description { get; set; }
        public DateOnly Published { get; set; }
        public decimal Price { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set;}
        public string Pdf { get; set; }
    }
}
