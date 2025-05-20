using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;

namespace PlaySpotApi.Routes
{
    public static class FullnessRoutes
    {
        public static RouteGroupBuilder MapFullnessRoutes(this RouteGroupBuilder group)
        {
            // group.MapGet("/locationActivities", async (PlaySpotDbContext db) =>
            // {
            //     var locations = await db.LocationActivities.ToListAsync();

            //     return Results.Ok(locations);
            // })
            // .WithName("GetLocationAvticities")
            // .WithOpenApi()
            // .Produces<List<LocationActivity>>(StatusCodes.Status200OK)
            // .Produces(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
