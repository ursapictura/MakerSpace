using Microsoft.EntityFrameworkCore;
using MakerSpace.Models;
using MakerSpace.Data;

namespace MakerSpace
{
    public class MakerSpaceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Storefront> Storefronts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FavoritePattern> FavoritePatterns { get; set; }
        public DbSet<LibraryPattern> LibraryPatterns { get; set; }
        public DbSet<PatternTag> PatternTags { get; set; }

        public MakerSpaceDbContext(DbContextOptions<MakerSpaceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(UserData.Users);
            modelBuilder.Entity<Category>().HasData(CategoryData.Categories);
            modelBuilder.Entity<Pattern>().HasData(PatternData.Patterns);
            modelBuilder.Entity<Library>().HasData(LibraryData.Libraries);
            modelBuilder.Entity<Storefront>().HasData(StorefrontData.Storefronts);
            modelBuilder.Entity<Tag>().HasData(TagData.Tags);
            modelBuilder.Entity<FavoritePattern>().HasData(FavoritePatternData.FavoritePatterns);
            modelBuilder.Entity<LibraryPattern>().HasData(LibraryPatternData.LibraryPatterns);
            modelBuilder.Entity<PatternTag>().HasData(PatternTagData.PatternTags);
        }
    }
}
