using MakerSpace.Models;
using MakerSpace.DTO;
using System.Security.Claims;

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
        }
    }
}
