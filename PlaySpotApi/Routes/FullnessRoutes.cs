using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;

namespace PlaySpotApi.Routes
{
    public static class FullnessRoutes
    {
        public static RouteGroupBuilder MapFullnessRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("/create", async ([AsParameters] FullnessQuery query, PlaySpotDbContext db) =>
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(query);
                if (!Validator.TryValidateObject(query, context, results, true))
                {
                    return Results.BadRequest(results);
                }

                var locationQuery = db.Locations
                    .AsQueryable();

                locationQuery = locationQuery
                    .Where(l => l.LocationId == query.LocationId);

                var location = await locationQuery.FirstOrDefaultAsync();
                if (location == null)
                {
                    return Results.NotFound("Location not found");
                }

                var fullness = new Fullness
                {
                    DateTime = DateTime.UtcNow,
                    FullnessLevel = query.FullnessLevel,
                    Location = location
                };

                db.Fullness.Add(fullness);
                await db.SaveChangesAsync();

                return Results.Created($"/fullness/{fullness.FullnessId}", fullness);
            })
            .WithName("CreateFullness")
            .WithOpenApi()
            .Produces<Fullness>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
