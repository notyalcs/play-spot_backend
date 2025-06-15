using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Location.Api.Data;
using Location.Api.Queries;
using Location.Api.Helpers;

namespace Location.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationDbContext _context;

        public LocationController(LocationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Location>>> GetLocations([FromQuery] LocationQuery query)
        {
            var locations = await _context.Locations.ToListAsync();

            if (query?.Latitude is double lat && query.Longitude is double lon)
            {
                locations = locations
                    .Where(l => GeoHelper.IsWithinRadius(
                        lat, lon,
                        l.Latitude, l.Longitude,
                        query.Radius ?? 10))
                    .ToList();
            }

            return Ok(locations);
        }
    }
}
