using MakerSpace.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace MakerSpace.Endpoints
{
    public static class PatternEndpoints
    {
        public static void MapPatternEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/pattern").WithTags(nameof(Pattern));

            routes.MapGet("", async (MakerSpaceDbContext db) =>
            {
                var patterns = await db.Patterns
                    .Include(p => p.Maker)
                    .Include(p => p.Category)
                    .Include(p => p.PatternTags)
                        .ThenInclude(pt => pt.Tag)
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return Results.Ok(patterns);
            });

            // Get all patterns in a specific category
            routes.MapGet("/category/{categoryName}", async (MakerSpaceDbContext db, string categoryName) =>
            {

                // Check that Category exists
                var categoryExists = await db.Categories
                .AnyAsync(c => c.Name == categoryName);

                if (!categoryExists)
                {
                    return Results.NotFound($"Category '{categoryName}' not found");
                }
                ;

                var patterns = await db.Patterns
                    .Include(p => p.Maker)
                    .Include(p => p.Category)
                    .Include(p => p.PatternTags)
                        .ThenInclude(pt => pt.Tag)
                    .OrderBy(p => p.Id)
                    .Where(p => p.Category.Name == categoryName)
                    .ToListAsync();

                // if no patterns are found, return not found
                if (patterns.Count == 0)
                {
                    return Results.NotFound("No patterns in this category");
                }
                ;

                //if patterns are found, return patterns
                return Results.Ok(patterns);
            });

            // Get all patterns made by a specific seller
            routes.MapGet("/seller/{sellerId}", async (MakerSpaceDbContext db, int sellerId) =>
            {

                var sellerExists = await db.Users
                    .AnyAsync(u => u.Id == sellerId);

                if (!sellerExists)
                {
                    return Results.NotFound($"SellerId '{sellerId}' not found");
                }

                var patterns = await db.Patterns
                    .Include(p => p.Maker)
                    .Include(p => p.Category)
                    .Include(p => p.PatternTags)
                        .ThenInclude(pt => pt.Tag)
                    .OrderBy(p => p.Id)
                    .Where(p => p.MakerId == sellerId)
                    .ToListAsync();

                if (patterns == null)
                {
                    return Results.NotFound("no patterns found for this seller");
                }

                return Results.Ok(patterns);
            });

            //Get all patterns that match a search query
            routes.MapGet("/search/{searchInput}", async (MakerSpaceDbContext db, string searchInput) =>
            {
                var patterns = await db.Patterns
                    .Include(p => p.Maker)
                    .Include(p => p.Category)
                    .Include(p => p.PatternTags)
                        .ThenInclude(pt => pt.Tag)
                    .OrderBy(p => p.Id)
                    .Where(p =>
                        p.Name.ToLower().Contains(searchInput.ToLower()) ||
                        p.Maker.UserName.ToLower().Contains(searchInput.ToLower()) ||
                        p.Category.Name.ToLower().Contains(searchInput.ToLower()) ||
                        p.PatternTags.Any(pt => pt.Tag != null && pt.Tag.Name.ToLower().Contains(searchInput.ToLower()))
                    )
                    .ToListAsync();
            });

            // Get single pattern
            routes.MapGet("/{patternId}", async (MakerSpaceDbContext db, int patternId) =>
            {
                Pattern? pattern = await db.Patterns
                    .Include(p => p.Maker)
                    .Include(p => p.Category) 
                    .Include(p => p.PatternTags) 
                        .ThenInclude(pt => pt.Tag)
                    .FirstOrDefaultAsync(p => p.Id == patternId);

                if (pattern == null)
                {
                    return Results.NotFound($"A pattern with Id:{patternId} does not exist.");
                };

                return Results.Ok(pattern);
            });

            // Create a new pattern
            routes.MapPost("", async (MakerSpaceDbContext db, Pattern newPattern) =>
            {
                
                if (newPattern == null)
                {
                    return Results.NotFound($"NewPattern couldnot be created");
                }
                else if (!db.Users.Any(u => u.Id == newPattern.MakerId))
                {
                    return Results.NotFound($"No makers were found with MakerId: {newPattern.MakerId}");
                }
                else if (!db.Categories.Any(c => c.Id == newPattern.CategoryId))
                {
                    return Results.NotFound($"No categories were found with CategoryId: {newPattern.CategoryId}");
                }

                Pattern addPattern = new()
                {
                    Name = newPattern.Name,
                    MakerId = newPattern.MakerId,
                    CategoryId = newPattern.CategoryId,
                    Description = newPattern.Description,
                    Published = DateOnly.FromDateTime(DateTime.UtcNow),
                    Price = newPattern.Price,
                    Photo1 = newPattern.Photo1,
                    Photo2 = newPattern.Photo2,
                    Photo3 = newPattern.Photo3,
                    Pdf = newPattern.Pdf,
                    IsApproved = false,
                    IsDeleted = false,
                };

                db.Patterns.Add(addPattern);
                db.SaveChanges();
                return Results.Created($"pattern/{addPattern.Id}", addPattern);

            });

            // Update a specific pattern
            routes.MapPut("/{patternId}", async (MakerSpaceDbContext db, int patternId, Pattern pattern) =>
            {
                Pattern? patternToUpdate = await db.Patterns.SingleOrDefaultAsync(p => p.Id == patternId);

                if (patternToUpdate == null)
                {
                    return Results.NotFound($"A pattern with Id:{patternId} does not exist.");
                }
                else if (!db.Users.Any(u => u.Id == pattern.MakerId))
                {
                    return Results.NotFound($"No makers were found with MakerId: {pattern.MakerId}");
                }
                else if (!db.Categories.Any(c => c.Id == pattern.CategoryId))
                {
                    return Results.NotFound($"No categories were found with CategoryId: {pattern.CategoryId}");
                }

                patternToUpdate.Name = pattern.Name;
                patternToUpdate.MakerId = pattern.MakerId;
                patternToUpdate.CategoryId = pattern.CategoryId;
                patternToUpdate.Description = pattern.Description;
                patternToUpdate.Price = pattern.Price;
                patternToUpdate.Photo1 = pattern.Photo1;
                patternToUpdate.Photo2 = pattern.Photo2;
                patternToUpdate.Photo3 = pattern.Photo3;
                patternToUpdate.Pdf = pattern.Pdf;
                patternToUpdate.IsApproved = false;
                patternToUpdate.IsDeleted = false;

                db.SaveChanges();
                return Results.Ok(patternToUpdate);
            });

            // Soft Delete a Pattern, this will make is visible only to users who have purchased it
            routes.MapPatch("/delete/{patternId}", async (MakerSpaceDbContext db, int patternId) =>
            {
                Pattern? patternToSoftDelete = await db.Patterns.SingleOrDefaultAsync(p => p.Id == patternId);

                if (patternToSoftDelete == null)
                {
                    return Results.NotFound($"A pattern with Id:{patternId} does not exist.");
                }

                patternToSoftDelete.IsDeleted = true;

                db.SaveChanges();
                return Results.Ok(patternToSoftDelete);
            });
        }

    }
} 
