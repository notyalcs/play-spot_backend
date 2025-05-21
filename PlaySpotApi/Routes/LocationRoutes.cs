using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;
using PlaySpotApi.Helpers;
using PlaySpotApi.DTOs;


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

                var now = DateTime.UtcNow;
                var cutoffTime = now.AddHours(-2);

                var locationsQuery = db.Locations
                    .Include(l => l.Sports)
                    .Include(l => l.Fullness)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(query.SportName))
                {
                    locationsQuery = locationsQuery
                        .Where(l => l.Sports.Any(s => s.Name.ToLower() == query.SportName.ToLower()));
                }
                
                var locations = await locationsQuery.ToListAsync();

                if (query.Latitude.HasValue && query.Longitude.HasValue)
                {
                    // TODO: Make the calculation inline so SQL can handle it if data set is large
                    locations = locations
                        .Where(L => GeoHelper.IsWithinRadius(
                            query.Latitude.Value, query.Longitude.Value,
                            L.Latitude, L.Longitude,
                            query.Radius ?? 10))
                        .ToList();
                }

                var locationDTOs = locations.Select(location =>
                {
                    var recent = location.Fullness
                        .Where(f => f.DateTime >= cutoffTime)
                        .OrderByDescending(f => f.DateTime)
                        .ToList();

                    double weightedSum = 0;
                    double weightTotal = 0;

                    foreach (var f in recent)
                    {
                        var minutesSince = (now - f.DateTime).TotalMinutes;
                        var weight = 120 - minutesSince;
                        if (weight < 0) continue;

                        weightedSum += (int)f.FullnessLevel * weight;
                        weightTotal += weight;
                    }

                    var averageFullness = weightTotal > 0
                        ? (int)weightedSum / weightTotal
                        : 0;
                    var scaledScore = (int)Math.Round(averageFullness / 4.0 * 100);

                    return new LocationDTO
                    {
                        LocationId = location.LocationId,
                        Name = location.Name,
                        Address = location.Address,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Sports = [.. location.Sports],//.Select(s => s).ToList(),
                        FullnessScore = scaledScore
                    };
                }).ToList();

                return Results.Ok(locationDTOs);
            })
            .WithName("GetLocations")
            .WithOpenApi()
            .Produces<List<LocationDTO>>(StatusCodes.Status200OK)
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
