using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Location.Api.Data;
using Location.Api.Queries;
using Location.Api.Helpers;
using Location.Api.DTOs;

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

        // GET /api/Location
        // GET /api/Location?latitude=...&longitude=...&radius=10
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

        // GET /api/Location/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Location>> GetLocations(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // GET /api/Location/simple
        [HttpGet("simple")]
        public async Task<ActionResult<List<LocationDTO>>> GetSimpleLocations()
        {
            var locations = await _context.Locations
                .Select(l => new LocationDTO(
                    l.LocationId,
                    l.Name?? "",
                    l.Address?? "",
                    l.Latitude,
                    l.Longitude
                ))
                .ToListAsync();

            return Ok(locations);
        }

        // POST /api/Location
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> CreateLocation([FromBody] LocationDTO locationDto)
        {
            if (locationDto == null)
            {
                return BadRequest("Location data is required.");
            }

            var location = new Models.Location
            {
                Name = locationDto.Name,
                Address = locationDto.Address,
                Latitude = locationDto.Latitude,
                Longitude = locationDto.Longitude
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var createdDTO = new LocationDTO(
                location.LocationId,
                location.Name ?? "",
                location.Address ?? "",
                location.Latitude,
                location.Longitude
            );

            return CreatedAtAction(null, createdDTO);
        }

        // PUT /api/Location/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDTO locationDto)
        {
            if (locationDto == null || id != locationDto.LocationId)
            {
                return BadRequest("Invalid location data.");
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound("Location not fount.");
            }

            location.Name = locationDto.Name;
            location.Address = locationDto.Address;
            location.Latitude = locationDto.Latitude;
            location.Longitude = locationDto.Longitude;

            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/Location/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound("Location not found.");
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
