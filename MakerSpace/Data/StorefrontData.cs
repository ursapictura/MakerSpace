using MakerSpace.Models;

namespace MakerSpace.Data
{
    public class StorefrontData
    {
        public static List<Storefront> Storefronts = new()
        {
            new Storefront
            {
                Id = 1,
                Name = "Emily's Designs",
                SellerId = 1,
                Description = "Unique, hand-crafted sewing patterns by Emily Johnson.",
                BannerPhoto = "https://d2culxnxbccemt.cloudfront.net/knit/content/uploads/2025/05/23175938/banner-home-patterns.jpg"
            },
            new Storefront
            {
                Id = 2,
                Name = "Knit & Pearl Boutique",
                SellerId = 3,
                Description = "Trendy knitting patterns for all skill levels.",
                BannerPhoto = "https://images.ctfassets.net/ivgal0r4n3ub/5fmLOvNNA3YIKYdJsv8NVV/c999033ed0e21c867162d43cb3ad800a/Knitting_Banner.jpg"
            },
            new Storefront
            {
                Id = 3,
                Name = "Laura's Pattern Pro",
                SellerId = 5,
                Description = "Professional-grade patterns for serious hobbyists.",
                BannerPhoto = "https://images.squarespace-cdn.com/content/v1/5ddee0943d393a7e06173756/1674234278148-SZWPAT670GIYS1US0YGQ/Blog+Post+Banner+Journal+in+Yarn+How+to+Plan+a+Temperature+Blanket+Knitting+Project.png?format=1000w"
            },
            new Storefront
            {
                Id = 4,
                Name = "Crafty Mike's Market",
                SellerId = 2,
                Description = "Exclusive marketplace items curated by Mike.",
                BannerPhoto = "https://images.squarespace-cdn.com/content/v1/5ddee0943d393a7e06173756/1674666723668-EYEKOQRTYN7BEL68RXK3/Blog+Post+Banner+Temperature+Blanket+How+to+Knit+in+Garter+Stitch+Fifty+Four+Ten+Studio+Worsted+Yarn+2023.png?format=1000w"
            },
            new Storefront
            {
                Id = 5,
                Name = "DIY Dan's Workshop",
                SellerId = 4,
                Description = "DIY patterns and tools for beginners and pros alike.",
                BannerPhoto = "https://previews.123rf.com/images/irishasel/irishasel1808/irishasel180800506/107485960-long-banner-a-cute-baby-lies-on-a-basket-with-tangles-of-knitting-threads-hendmeid-of-multi.jpg"
            }
        };
    }
}
