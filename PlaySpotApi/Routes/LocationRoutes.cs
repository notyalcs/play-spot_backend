using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;

namespace PlaySpotApi.Routes
{
    public static class LocationRoutes
    {
        public static RouteGroupBuilder MapLocationRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (PlaySpotDbContext db) =>
            {
                var locations = await db.Locations
                    .Include(l => l.Sports)
                    .ToListAsync();

                return Results.Ok(locations);
            })
            .WithName("GetLocations")
            .WithOpenApi()
            .Produces<List<Location>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/create", async (PlaySpotDbContext db, Location location) =>
            {
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return Results.Created($"/locations/{location.LocationId}", location);
            })
            .WithName("CreateLocation")
            .WithOpenApi()
            .Produces<Location>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/by-sport/{sportName}", async (PlaySpotDbContext db, string sportName) =>
            {
                var locations = await db.Locations
                    .Include(l => l.Sports)
                    .Where(l => l.Sports.Any(s => s.Name.ToLower() == sportName.ToLower()))
                    .Select(l => new {
                        l.LocationId,
                        l.Name,
                        l.Address,
                        l.Coordinates,
                        Sports = l.Sports.Select(s => new { s.SportId, s.Name }).ToList()
                    })
                    .ToListAsync();

                return Results.Ok(locations);
            })
            .WithName("GetLocationsBySport")
            .WithOpenApi()
            .Produces<List<Location>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            return group;
        }
    }
}
