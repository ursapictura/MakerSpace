using MakerSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Endpoints
{
    public static class TagEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/tag").WithTags(nameof(Tag));

            // Get all tags
            routes.MapGet("", async (MakerSpaceDbContext db) =>
            {
                var tags = await db.Tags.ToListAsync();

                if (tags.Count == 0)
                {
                    return Results.NoContent();
                }

                return Results.Ok(tags);
            });

            // Create a Tag
            routes.MapPost("", async (MakerSpaceDbContext db, Tag newTag) =>
            {
                Tag addTag = new()
                {
                    Name = newTag.Name,
                };

                db.Tags.Add(addTag);
                db.SaveChanges();
                return Results.Created($"tag/{addTag.Id}", addTag);
            });
        }
    }
}
