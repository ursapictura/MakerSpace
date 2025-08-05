using MakerSpace.DTO;
using MakerSpace.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MakerSpace.API
{
    public class LibraryPatternAPI
    {
        public static async void Map(WebApplication app)
        {
            app.MapPost("/api/library/{libraryId}/patterns", async (MakerSpaceDbContext db, int libraryId, CreatePatternDTO request, HttpContext httpContext) =>
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

                // Check if the library exists and belongs to the user
                var library = await db.Libraries
                    .Include(l => l.LibraryPatterns)
                    .FirstOrDefaultAsync(l => l.Id == libraryId && l.UserId == userId);
                if (library == null)
                {
                    return Results.NotFound($"Library with ID {libraryId} not found or not owned by user.");
                }

                // Check if the pattern exists
                var pattern = await db.Patterns.FindAsync(request.PatternId);
                if (pattern == null)
                {
                    return Results.NotFound($"Pattern with ID {request.PatternId} not found.");
                }

                // Check if the pattern is already in the library
                if (library.LibraryPatterns.Any(lp => lp.PatternId == request.PatternId))
                {
                    return Results.Conflict($"Pattern with ID {request.PatternId} is already in the library.");
                }

                // Optional: Validate purchase
                // var purchase = await db.Purchases.AnyAsync(p => p.PatternId == request.PatternId && p.UserId == userId);
                // if (!purchase)
                // {
                //     return Results.BadRequest("Pattern has not been purchased.");
                // }

                // Create the LibraryPattern entry
                var libraryPattern = new LibraryPattern
                {
                    LibraryId = libraryId,
                    PatternId = request.PatternId
                };
                library.LibraryPatterns.Add(libraryPattern);

                try
                {
                    await db.SaveChangesAsync();
                    return Results.Created($"/api/library/{libraryId}/patterns/{request.PatternId}", libraryPattern);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return Results.StatusCode(500);
                }
            })
            .RequireAuthorization()
            .WithName("AddPatternToLibrary")
            .WithOpenApi();

            // READ: GET /api/library/{libraryId}/patterns
            app.MapGet("/api/library/{libraryId}/patterns", async (MakerSpaceDbContext db, int libraryId, HttpContext httpContext) =>
            {
                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Check if the library exists and belongs to the user
                var library = await db.Libraries
                    .Include(l => l.LibraryPatterns)
                    .ThenInclude(lp => lp.Pattern)
                    .FirstOrDefaultAsync(l => l.Id == libraryId && l.UserId == userId);
                if (library == null)
                {
                    return Results.NotFound($"Library with ID {libraryId} not found or not owned by user.");
                }

                // Return the library patterns
                return Results.Ok(library.LibraryPatterns);
            })
            .RequireAuthorization()
            .WithName("GetLibraryPatterns")
            .WithOpenApi();
        }
    }
}
