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
        public DbSet<Favorites> Favorites { get; set; }
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

            modelBuilder.Entity<Library>()
                .HasOne(l => l.User)
                .WithOne(u => u.Library)
                .HasForeignKey<Library>(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.User)
                .WithOne(u => u.Favorites)
                .HasForeignKey<Favorites>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Storefront>()
                .HasOne(s => s.Seller)
                .WithOne(s => s.Storefront)
                .HasForeignKey<Storefront>(s => s.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LibraryPattern>()
               .HasOne(lp => lp.Library)
               .WithMany(l => l.LibraryPatterns)
               .HasForeignKey(lp => lp.LibraryId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoritePattern>()
                .HasOne(fp => fp.Favorites)
                .WithMany(f => f.FavoritePatterns)
                .HasForeignKey(fp => fp.FavoritesId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
