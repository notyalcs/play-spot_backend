using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport.Api.Data;

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
