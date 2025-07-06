namespace MakerSpace.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public List<Pattern> Patterns { get; set; }
    }
}
