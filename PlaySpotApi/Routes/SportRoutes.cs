using Microsoft.EntityFrameworkCore;

using PlaySpotApi.Data;
using PlaySpotApi.Models;

namespace PlaySpotApi.Routes
{
    public static class SportRoutes
    {
        public static RouteGroupBuilder MapSportRoutes(this RouteGroupBuilder group)
        {
            //this get request returns all distinct sports 
            group.MapGet("/", async (PlaySpotDbContext db) =>
            {
                var sportNames = await db.Sports
                    .Select(s => s.Name)
                    .Distinct()
                    .ToListAsync();

                return Results.Ok(sportNames);
            })
            .WithName("GetDistinctSportNames")
            .WithOpenApi()
            .Produces<List<string>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);



            return group;
        }
    }
}
