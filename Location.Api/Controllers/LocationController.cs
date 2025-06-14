using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using Location.Api.Data;
using Location.Api.Models;
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
        public async Task<ActionResult<List<Models.Location>>> GetLocations([AsParameters] LocationQuery query)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(query);
            if (!Validator.TryValidateObject(query, context, results, true))
            {
                return BadRequest(results);
            }

            var locationQuery = _context.Locations
                .AsQueryable();

            var locations = await locationQuery.ToListAsync();

            if (query.Latitude.HasValue && query.Longitude.HasValue)
            {
                locations = locations
                    .Where(l => GeoHelper.IsWithinRadius(
                        query.Latitude.Value, query.Longitude.Value,
                        l.Latitude, l.Longitude,
                        query.Radius ?? 10))
                    .ToList();
            }

            return Ok(locations);
        }
    }
}
