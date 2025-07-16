using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class UserData
    {
        public static List<User> Users = new()
        {
            new User
            {
                Id = 1,
                UserName = "sewingQueen",
                FirstName = "Emily",
                LastName = "Johnson",
                Email = "emily.johnson@example.com",
                IsSeller = true
            },
            new User
            {
                Id = 2,
                UserName = "craftyMike",
                FirstName = "Michael",
                LastName = "Smith",
                Email = "mike.smith@example.com",
                IsSeller = false
            },
            new User
            {
                Id = 3,
                UserName = "knitAndPearl",
                FirstName = "Sarah",
                LastName = "Williams",
                Email = "sarah.williams@example.com",
                IsSeller = true
            },
            new User
            {
                Id = 4,
                UserName = "diyDan",
                FirstName = "Daniel",
                LastName = "Brown",
                Email = "dan.brown@example.com",
                IsSeller = false
            },
            new User
            {
                Id = 5,
                UserName = "patternPro",
                FirstName = "Laura",
                LastName = "Davis",
                Email = "laura.davis@example.com",
                IsSeller = true
            }
        };
    }
}
