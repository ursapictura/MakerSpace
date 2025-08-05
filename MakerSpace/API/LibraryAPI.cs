using MakerSpace.Models;
using MakerSpace.DTO;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.API
{
    // LibraryAPI.cs
    public class LibraryAPI
    {
        public static async void Map(WebApplication app)
        {
            app.MapPost("/api/library", async (MakerSpaceDbContext db, CreateLibraryDTO request, HttpContext httpContext) =>
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

                // Check if the user exists
                var user = await db.Users.FindAsync(userId);
                if (user == null)
                {
                    return Results.NotFound($"User with ID {userId} not found.");
                }

                // Check if the pattern exists
                var pattern = await db.Patterns.FindAsync(request.PatternId);
                if (pattern == null)
                {
                    return Results.NotFound($"Pattern with ID {request.PatternId} not found.");
                }

                // Optional: Validate purchase
                // var purchase = await db.Purchases.AnyAsync(p => p.PatternId == request.PatternId && p.UserId == userId);
                // if (!purchase)
                // {
                //     return Results.BadRequest("Pattern has not been purchased.");
                // }

                // Create the library
                var library = new Library
                {
                    UserId = userId,
                    LibraryPatterns = new List<LibraryPattern>()
                };

                // Create the LibraryPattern entry
                var libraryPattern = new LibraryPattern
                {
                    Library = library,
                    Pattern = pattern
                };
                library.LibraryPatterns.Add(libraryPattern);

                try
                {
                    db.Libraries.Add(library);
                    await db.SaveChangesAsync();
                    return Results.Created($"/api/library/{library.Id}", library);
                }
                catch (Exception ex)
                {
                    // Log the exception (use Serilog in production)
                    return Results.StatusCode(500);
                }
            })
            .RequireAuthorization()
            .WithName("CreateLibrary")
            .WithOpenApi();

            // READ: GET /api/library/{id}
            app.MapGet("/api/library/{id}", async (MakerSpaceDbContext db, int id, HttpContext httpContext) =>
            {
                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Find the library, ensuring it belongs to the user
                var library = await db.Libraries
                    .Include(l => l.LibraryPatterns)
                    .ThenInclude(lp => lp.Pattern)
                    .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

                if (library == null)
                {
                    return Results.NotFound($"Library with ID {id} not found or not owned by user.");
                }

                // Return the library with its patterns
                return Results.Ok(library);
            })
            .RequireAuthorization()
            .WithName("GetLibrary")
            .WithOpenApi();

            // UPDATE: PUT /api/library/{id}
            app.MapPut("/api/library/{id}", async (MakerSpaceDbContext db, int id, UpdateLibraryDTO request, HttpContext httpContext) =>
            {
                // Validate input
                if (request.PatternIds == null || !request.PatternIds.Any())
                {
                    return Results.BadRequest("At least one Pattern ID is required.");
                }

                // Get the authenticated user's ID
                var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    return Results.Unauthorized();
                }

                // Find the library, ensuring it belongs to the user
                var library = await db.Libraries
                    .Include(l => l.LibraryPatterns)
                    .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);

                if (library == null)
                {
                    return Results.NotFound($"Library with ID {id} not found or not owned by user.");
                }

                // Validate all pattern IDs
                var patterns = await db.Patterns
                    .Where(p => request.PatternIds.Contains(p.Id))
                    .ToListAsync();

                if (patterns.Count != request.PatternIds.Count)
                {
                    var invalidIds = request.PatternIds.Except(patterns.Select(p => p.Id)).ToList();
                    return Results.BadRequest($"Invalid Pattern IDs: {string.Join(", ", invalidIds)}");
                }

                // Optional: Validate purchases
                // var purchases = await db.Purchases
                //     .Where(p => p.UserId == userId && request.PatternIds.Contains(p.PatternId))
                //     .Select(p => p.PatternId)
                //     .ToListAsync();
                // if (purchases.Count != request.PatternIds.Count)
                // {
                //     var unpurchasedIds = request.PatternIds.Except(purchases).ToList();
                //     return Results.BadRequest($"Patterns not purchased: {string.Join(", ", unpurchasedIds)}");
                // }

                try
                {
                    // Update LibraryPatterns: Remove existing, add new
                    db.LibraryPatterns.RemoveRange(library.LibraryPatterns);
                    library.LibraryPatterns = request.PatternIds
                        .Select(patternId => new LibraryPattern
                        {
                            LibraryId = library.Id,
                            PatternId = patternId
                        })
                        .ToList();

                    await db.SaveChangesAsync();
                    return Results.Ok(library);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return Results.StatusCode(500);
                }
            })
            .RequireAuthorization()
            .WithName("UpdateLibrary")
            .WithOpenApi();
        }

    }
}
