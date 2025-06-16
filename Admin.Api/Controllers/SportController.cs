using Microsoft.AspNetCore.Mvc;

using Admin.Api.DTOs;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class SportController : ControllerBase
    {
        private readonly IHttpClientFactory _http;

        public SportController(IHttpClientFactory http) => _http = http;

        // POST /api/Admin/sport
        [HttpPost]
        public async Task<IActionResult> CreateSport([FromBody] SportDTO sport)
        {
            if (sport == null)
            {
                return BadRequest("Sport data is required.");
            }

            var client = _http.CreateClient("SportService");
            var response = await client.PostAsJsonAsync("api/Sport", sport);

            if (response.IsSuccessStatusCode)
            {
                var createdSport = await response.Content.ReadFromJsonAsync<SportDTO>();
                return CreatedAtAction(null, createdSport);
            }

            return StatusCode((int)response.StatusCode, "Failed to create sport.");
        }

        // // PUT /api/Admin/sport/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateSport(int id, [FromBody] SportDTO sport)
        // {
        //     if (sport == null || id <= 0)
        //     {
        //         return BadRequest("Invalid sport data or ID.");
        //     }

        //     var client = _http.CreateClient("SportService");
        //     var response = await client.PutAsJsonAsync($"api/Sport/{id}", sport);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         var updatedSport = await response.Content.ReadFromJsonAsync<SportDTO>();
        //         return Ok(updatedSport);
        //     }

        //     return StatusCode((int)response.StatusCode, "Failed to update sport.");
        // }

        // DELETE /api/Admin/sport/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid sport ID.");
            }

            var client = _http.CreateClient("SportService");
            var response = await client.DeleteAsync($"api/Sport/{id}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }

            return StatusCode((int)response.StatusCode, "Failed to delete sport.");
        }
    }
}
