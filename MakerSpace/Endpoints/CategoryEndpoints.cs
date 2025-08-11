using Microsoft.EntityFrameworkCore;
using MakerSpace.Models;

namespace MakerSpace.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/category").WithTags(nameof(Category));

            group.MapGet("/", async (MakerSpaceDbContext db) =>
            {
                var categories = await db.Categories.ToListAsync();

                if (categories.Count == 0) 
                {
                    return Results.NoContent();
                }

                return Results.Ok(categories);
            });
        }
    }
}
