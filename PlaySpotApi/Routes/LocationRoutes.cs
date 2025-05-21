using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;
using PlaySpotApi.Helpers;


namespace PlaySpotApi.Routes
{
    public static class LocationRoutes
    {
        public static RouteGroupBuilder MapLocationRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", async ([AsParameters] LocationQuery query, PlaySpotDbContext db) =>
            {
                var results = new List<ValidationResult>();
                var context = new ValidationContext(query);
                if (!Validator.TryValidateObject(query, context, results, true))
                {
                    return Results.BadRequest(results);
                }

                var locationsQuery = db.Locations
                    .Include(l => l.Sports)
                    .Include(l => l.Fullness)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(query.SportName))
                {
                    locationsQuery = locationsQuery
                        .Where(l => l.Sports.Any(s => s.Name.ToLower() == query.SportName.ToLower()));
                }

                if (query.Latitude.HasValue && query.Longitude.HasValue)
                {
                    var filtered = await locationsQuery.ToListAsync();
                    // TODO: Make the calculation inline so SQL can handle it if data set is large
                    filtered = filtered
                        .Where(L => GeoHelper.IsWithinRadius(
                            query.Latitude.Value, query.Longitude.Value,
                            L.Latitude, L.Longitude,
                            query.Radius ?? 10))
                        .ToList();
                    
                    return Results.Ok(filtered);
                }

                var locations = await locationsQuery.ToListAsync();
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

            return group;
        }
    }
}
