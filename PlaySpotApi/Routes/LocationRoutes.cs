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


            //this get request returns all the locations for a given sport.
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



            //Aaryan
            //this get request returns all the locations for a given sport with a specified radius.
            group.MapGet("/locations-by-sport", async (
                string sportName,
                string coordinates,   
                double radius,        // in km
                PlaySpotDbContext db) =>
            {
                // 1) Parse the user's coordinates
                var userParts = coordinates
                    .Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (userParts.Length != 2
                    || !double.TryParse(userParts[0], out var userLat)
                    || !double.TryParse(userParts[1], out var userLon))
                {
                    return Results.BadRequest("Invalid 'coordinates' format. Use 'lat,lon'.");
                }

                // 2) Fetch all locations that offer the sport
                var all = await db.Locations
                    .Include(l => l.Sports)
                    .Where(l => l.Sports
                        .Any(s => s.Name
                            .Equals(sportName, StringComparison.OrdinalIgnoreCase)))
                    .Select(l => new
                    {
                        l.LocationId,
                        l.Name,
                        l.Address,
                        l.Coordinates,   // still as "lat,lon"
                        Sports = l.Sports
                            .Select(s => new { s.SportId, s.Name })
                            .ToList()
                    })
                    .ToListAsync();

                // 3) Parse each location's coords, filter by radius
                var withinRadius = new List<object>();
                foreach (var loc in all)
                {
                    if (string.IsNullOrWhiteSpace(loc.Coordinates))
                        continue; // skip null or empty

                    var parts = loc.Coordinates
                        .Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2
                        || !double.TryParse(parts[0], out var lat)
                        || !double.TryParse(parts[1], out var lon))
                    {
                        continue; // skip malformed entry
                    }

                    if (GeoHelper.IsWithinRadius(
                            userLat, userLon,
                            lat, lon,
                            radius))
                    {
                        withinRadius.Add(loc);
                    }
                }

                // 4) Return only those within the radius
                return Results.Ok(withinRadius);
            })
            .WithName("GetLocationsBySportAndRadius")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);



            return group;
        }
    }
}
