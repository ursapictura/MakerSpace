using Microsoft.EntityFrameworkCore;
using MakerSpace.Models;

namespace MakerSpace.Endpoints
{
    public static class StorefrontEndpoints
    {
        public static void MapStorefrontEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/storefronts").WithTags(nameof(Storefront));

            // Get storefront by Id
            group.MapGet("/{storefrontId}", async (MakerSpaceDbContext db, int storefrontId) =>
            {
                Storefront selectedStorefront = await db.Storefronts
                    .Include(storefront => storefront.Seller)
                    .ThenInclude(seller => seller.Patterns)
                    .FirstOrDefaultAsync(storefront => storefront.Id == storefrontId);

                if (selectedStorefront == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(selectedStorefront);
            });

            // Create storefront
            group.MapPost("/", async (MakerSpaceDbContext db, Storefront storefront) =>
            {
                db.Storefronts.Add(storefront);
                await db.SaveChangesAsync();
                return Results.Created($"/storefronts/{storefront.Id}", storefront);
            });

            // Update storefront
            group.MapPatch("/{storefontId}", async (MakerSpaceDbContext db, int storefrontId, Storefront updatedStorefront) =>
            {
                Storefront storefrontToUpdate = await db.Storefronts.FirstOrDefaultAsync(storefront => storefront.Id == storefrontId);

                if (storefrontToUpdate == null)
                {
                    return Results.NotFound();
                }

                storefrontToUpdate.SellerId = updatedStorefront.SellerId;
                storefrontToUpdate.Name = updatedStorefront.Name;
                storefrontToUpdate.Description = updatedStorefront.Description;
                storefrontToUpdate.BannerPhoto = updatedStorefront.BannerPhoto;

                await db.SaveChangesAsync();
                return Results.Ok(updatedStorefront);
            });

            // Delete storefront
            group.MapDelete("/{storefrontId}", async (MakerSpaceDbContext db, int storefrontId) =>
            {
                var storefrontToDelete = await db.Storefronts.FirstOrDefaultAsync(storefront => storefront.Id == storefrontId);

                if (storefrontToDelete == null)
                {
                    return Results.NotFound();
                }

                db.Storefronts.Remove(storefrontToDelete);
                await db.SaveChangesAsync();
                return Results.Ok($"Storefront Id {storefrontId} removed.");
            });
        }
    }
}
