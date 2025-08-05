using MakerSpace.DTO;
using MakerSpace.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MakerSpace.API
{
    public class FavoritesAPI
    {
        public static async void Map(WebApplication app)
        {
            // CREATE: POST /api/favorites/{favoritesId}/patterns
            app.MapPost("/api/favorites/{favoritesId}/patterns", async (MakerSpaceDbContext db, int favoritesId, CreatePatternDTO request, HttpContext httpContext) =>
            {
                // Validate input
                if (request.PatternId <= 0)
                {
                    return Results.BadRequest("Valid Pattern ID is required.");
                }

                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Check if the favorites collection exists and belongs to the user
                var favorites = await db.Favorites
                    .Include(f => f.FavoritePatterns)
                    .FirstOrDefaultAsync(f => f.Id == favoritesId && f.UserId == userId);
                if (favorites == null)
                {
                    return Results.NotFound($"Favorites with ID {favoritesId} not found or not owned by user.");
                }

                // Check if the pattern exists
                var pattern = await db.Patterns.FindAsync(request.PatternId);
                if (pattern == null)
                {
                    return Results.NotFound($"Pattern with ID {request.PatternId} not found.");
                }

                // Check if the pattern is already in the favorites
                if (favorites.FavoritePatterns.Any(fp => fp.PatternId == request.PatternId))
                {
                    return Results.Conflict($"Pattern with ID {request.PatternId} is already in the favorites.");
                }

                // Optional: Validate purchase
                // var purchase = await db.Purchases.AnyAsync(p => p.PatternId == request.PatternId && p.UserId == userId);
                // if (!purchase)
                // {
                //     return Results.BadRequest("Pattern has not been purchased.");
                // }

                // Create the FavoritePattern entry
                var favoritePattern = new FavoritePattern
                {
                    FavoritesId = favoritesId,
                    PatternId = request.PatternId
                };
                favorites.FavoritePatterns.Add(favoritePattern);

                try
                {
                    await db.SaveChangesAsync();
                    return Results.Created($"/api/favorites/{favoritesId}/patterns/{request.PatternId}", favoritePattern);
                }
                catch (Exception ex)
                {
                    // Log the exception (use Serilog in production)
                    return Results.StatusCode(500);
                }
            })
            .RequireAuthorization()
            .WithName("AddFavoritePattern")
            .WithOpenApi();

            // READ: GET /api/favorites/{favoritesId}/patterns
            app.MapGet("/api/favorites/{favoritesId}/patterns", async (MakerSpaceDbContext db, int favoritesId, HttpContext httpContext) =>
            {
                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Check if the favorites collection exists and belongs to the user
                var favorites = await db.Favorites
                    .Include(f => f.FavoritePatterns)
                    .ThenInclude(fp => fp.Pattern)
                    .FirstOrDefaultAsync(f => f.Id == favoritesId && f.UserId == userId);
                if (favorites == null)
                {
                    return Results.NotFound($"Favorites with ID {favoritesId} not found or not owned by user.");
                }

                // Return the favorite patterns
                return Results.Ok(favorites.FavoritePatterns);
            })
            .RequireAuthorization()
            .WithName("GetFavoritePatterns")
            .WithOpenApi();

            // DELETE: DELETE /api/favorites/{favoritesId}/patterns/{patternId}
            app.MapDelete("/api/favorites/{favoritesId}/patterns/{patternId}", async (MakerSpaceDbContext db, int favoritesId, int patternId, HttpContext httpContext) =>
            {
                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Check if the favorites collection exists and belongs to the user
                var favorites = await db.Favorites
                    .Include(f => f.FavoritePatterns)
                    .FirstOrDefaultAsync(f => f.Id == favoritesId && f.UserId == userId);
                if (favorites == null)
                {
                    return Results.NotFound($"Favorites with ID {favoritesId} not found or not owned by user.");
                }

                // Find the favorite pattern to delete
                var favoritePattern = favorites.FavoritePatterns
                    .FirstOrDefault(fp => fp.PatternId == patternId);
                if (favoritePattern == null)
                {
                    return Results.NotFound($"Pattern with ID {patternId} not found in favorites.");
                }

                try
                {
                    db.FavoritePatterns.Remove(favoritePattern);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return Results.StatusCode(500);
                }
            })
            .RequireAuthorization()
            .WithName("DeleteFavoritePattern")
            .WithOpenApi();
        }
    }
}
