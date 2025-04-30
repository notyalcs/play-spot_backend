using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;

namespace PlaySpotApi.Routes
{
    public static class LocationSportRoutes
    {
        public static IEndpointRouteBuilder MapLocationSportRoutes(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/locationSports", async (PlaySpotDbContext db) =>
            {
                var locations = await db.LocationSports
                    .Include(ls => ls.Location)
                    .Include(ls => ls.Sport)
                    .ToListAsync();

                return Results.Ok(locations);
            })
            .WithName("GetLocationSports")
            .WithOpenApi()
            .Produces<List<LocationActivity>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            return routes;
        }
    }
}
