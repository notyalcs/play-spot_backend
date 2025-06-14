using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using Fullness.Api.Data;
using Fullness.Api.Models;
using Fullness.Api.Queries;

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

            // var fullnessQuery = _context.Fullness
            //     .AsQueryable();

            // var locations = fullnessQuery
            //     .Where(l => l.LocationId == query.LocationId);

            // var location = await locationQuery.FirstOrDefaultAsync();
            // if (location == null)
            // {
            //     return NotFound("Location not found");
            // }

            var fullness = new Models.Fullness
            {
                DateTime = DateTime.UtcNow,
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
    }
}
