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
    }
}
