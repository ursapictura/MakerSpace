using Microsoft.EntityFrameworkCore;
using MakerSpace.Models;

namespace MakerSpace.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/users").WithTags(nameof(User));

            // Get user by Id
            group.MapGet("/{userId}", async (MakerSpaceDbContext db, int userId) =>
            {
                User selectedUser = await db.Users
                    .Include(user => user.Patterns)
                    .FirstOrDefaultAsync(user => user.Id == userId);

                if (selectedUser == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(selectedUser);
            });

            // Create user
            group.MapPost("/", async (MakerSpaceDbContext db, User user) =>
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Created($"/users/{user.Id}", user);
            });

            // Update user
            group.MapPatch("/{userId}", async (MakerSpaceDbContext db, int userId, User updatedUser) =>
            {
                User userToUpdate = await db.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if (userToUpdate == null)
                {
                    return Results.NotFound();
                }

                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.FirstName = updatedUser.FirstName;
                userToUpdate.LastName = updatedUser.LastName;
                userToUpdate.Email = updatedUser.Email;
                userToUpdate.IsSeller = updatedUser.IsSeller;

                await db.SaveChangesAsync();
                return Results.Ok(updatedUser);
            });

            // Delete user
            group.MapDelete("/{userId}", async (MakerSpaceDbContext db, int userId) =>
            {
                var userToDelete = await db.Users.FirstOrDefaultAsync(user => user.Id == userId);

                if (userToDelete == null)
                {
                    return Results.NotFound();
                }

                db.Users.Remove(userToDelete);
                await db.SaveChangesAsync();
                return Results.Ok($"User Id {userId} removed.");
            });
        }
    }
}
