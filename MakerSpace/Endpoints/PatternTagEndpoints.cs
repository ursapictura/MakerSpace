using MakerSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Endpoints
{
    public static class PatternTagEndpoints
    {
        public static void MapPatternTagEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/pattern").WithTags(nameof(PatternTag));

            group.MapPost("/addTag", async (MakerSpaceDbContext db, PatternTag newPatternTag) => {

                if (newPatternTag == null)
                {
                    return Results.NotFound($"a new PatterTag could not be created.");
                }
                else if (!db.Patterns.Any(p => p.Id == newPatternTag.PatternId))
                {
                    return Results.NotFound($"PatternId '{newPatternTag.PatternId}' does not exist.");
                }
                else if (!db.Tags.Any(t => t.Id == newPatternTag.TagId))
                {
                    return Results.NotFound($"TagId '{newPatternTag.TagId}' does not exist.");
                }

                PatternTag? addPatternTag = new()
                {
                    PatternId = newPatternTag.PatternId,
                    TagId = newPatternTag.TagId,
                };

                db.PatternTags.Add(addPatternTag);
                db.SaveChanges();
                return Results.Created($"patternTag 'addPatterntagId' created", addPatternTag);
            });

            group.MapDelete("/deleteTag", async (MakerSpaceDbContext db, int patternId, int tagId) =>
            {
                if (!db.Patterns.Any(p => p.Id == patternId))
                {
                    return Results.NotFound($"PatternId '{patternId}' does not exist.");
                }
                else if (!db.Tags.Any(t => t.Id == tagId))
                {
                    return Results.NotFound($"TagId '{tagId}' does not exist.");
                }

                PatternTag? patternTagToDelete = await db.PatternTags
                                                            .SingleOrDefaultAsync(pt =>
                                                                pt.PatternId == patternId &&
                                                                pt.TagId == tagId);

                if (patternTagToDelete == null)
                {
                    return Results.NotFound($"A PatternTag with TagId:{tagId} and PatternId:{patternId} does not exist.");
                }

                db.PatternTags.Remove(patternTagToDelete);
                db.SaveChanges();

                return Results.NoContent();
            });
        }

    }
}
