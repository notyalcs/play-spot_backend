using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using Fullness.Api.Data;
using Fullness.Api.Models;
using Fullness.Api.Queries;
using Microsoft.EntityFrameworkCore;

namespace Fullness.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FullnessController : ControllerBase
    {
        private readonly FullnessDbContext _context;

        public FullnessController(FullnessDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFullness([AsParameters] FullnessQuery query)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(query);
            if (!Validator.TryValidateObject(query, context, results, true))
            {
                return BadRequest(results);
            }

            var fullness = new Models.Fullness
            {
                TimeStamp = DateTime.UtcNow,
                FullnessLevel = query.FullnessLevel,
                LocationId = query.LocationId
            };

            _context.Fullness.Add(fullness);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateFullness), new { id = fullness.FullnessId }, fullness);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving data: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFullness([FromQuery] int locationId)
        {
            if (locationId <= 0)
            {
                return BadRequest("Invalid location ID.");
            }

            var fullnessList = await _context.Fullness
                .Where(f => f.LocationId == locationId)
                .OrderByDescending(f => f.TimeStamp)
                .ToListAsync();

            return Ok(fullnessList);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentFullness([FromQuery] int locationId, [FromQuery] int minues = 120)
        {
            if (locationId <= 0)
            {
                return BadRequest("Invalid location ID.");
            }

            var cutoffTime = DateTime.UtcNow.AddMinutes(-minues);
            var recentFullnessList = await _context.Fullness
                .Where(f => f.LocationId == locationId && f.TimeStamp >= cutoffTime)
                .OrderByDescending(f => f.TimeStamp)
                .ToListAsync();

            return Ok(recentFullnessList);
        }
    }
}
