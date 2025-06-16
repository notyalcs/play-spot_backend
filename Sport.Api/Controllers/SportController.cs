using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport.Api.Data;
using Sport.Api.DTOs;
using Sport.Api.Queries;

namespace Sport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportController : ControllerBase
    {
        private readonly SportDbContext _context;

        public SportController(SportDbContext context)
        {
            _context = context;
        }

        // GET /api/Sport
        // GET /api/Sport?locationId=1
        [HttpGet]
        public async Task<ActionResult<List<SportDTO>>> GetSports([FromQuery] SportQuery query)
        {
            var sports = _context.Sports.AsQueryable();
            var sportDTOs = await sports
                .Select(s => new SportDTO(
                    s.SportId,
                    s.LocationId,
                    s.Name))
                .ToListAsync();

            if (query?.LocationId is int locationId)
            {
                sportDTOs = sportDTOs
                    .Where(s => s.LocationId == locationId)
                    .ToList();
            }

            return Ok(sportDTOs);
        }

        [HttpGet("names")]
        public async Task<ActionResult<List<string>>> GetDistinctSportNames()
        {
            var sportNames = await _context.Sports
                .Select(s => s.Name)
                .Distinct()
                .ToListAsync();

            return Ok(sportNames);
        }

        // POST /api/Sport
        [HttpPost]
        public async Task<ActionResult<SportDTO>> CreateSport([FromBody] SportDTO sportDto)
        {
            if (sportDto == null)
            {
                return BadRequest("Sport data is required.");
            }

            var sport = new Models.Sport
            {
                LocationId = sportDto.LocationId,
                Name = sportDto.Name
            };

            _context.Sports.Add(sport);
            await _context.SaveChangesAsync();

            var createdDTO = new SportDTO(
                sport.SportId,
                sport.LocationId,
                sport.Name
            );

            return CreatedAtAction(null, createdDTO);
        }

        // // PUT /api/Sport/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateSport(int id, [FromBody] SportDTO sportDto)
        // {
        //     if (sportDto == null || id != sportDto.SportId)
        //     {
        //         return BadRequest("Invalid sport data.");
        //     }

        //     var sport = await _context.Sports.FindAsync(id);
        //     if (sport == null)
        //     {
        //         return NotFound("Sport not found.");
        //     }

        //     sport.LocationId = sportDto.LocationId;
        //     sport.Name = sportDto.Name;

        //     _context.Entry(sport).State = EntityState.Modified;
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // DELETE /api/Sport/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound("Sport not found.");
            }

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
