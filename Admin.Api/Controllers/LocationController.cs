using Microsoft.AspNetCore.Mvc;

using Admin.Api.DTOs;

namespace Admin.Api.Controllers
{
    [ApiController]
    [Route("api/Admin/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IHttpClientFactory _http;

        public LocationController(IHttpClientFactory http) => _http = http;

        // POST /api/Admin/location
        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDTO location)
        {
            if (location == null)
            {
                return BadRequest("Location data is required.");
            }

            var client = _http.CreateClient("LocationService");
            var response = await client.PostAsJsonAsync("api/Location", location);

            if (response.IsSuccessStatusCode)
            {
                var createdLocation = await response.Content.ReadFromJsonAsync<LocationDTO>();
                return CreatedAtAction(null, createdLocation);
            }

            return StatusCode((int)response.StatusCode, "Failed to create location.");
        }

        // PUT /api/Admin/location/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDTO location)
        {
            if (location == null || id <= 0)
            {
                return BadRequest("Invalid location data or ID.");
            }

            var client = _http.CreateClient("LocationService");
            var response = await client.PutAsJsonAsync($"api/Location/{id}", location);

            if (response.IsSuccessStatusCode)
            {
                var updatedLocation = await response.Content.ReadFromJsonAsync<LocationDTO>();
                return Ok(updatedLocation);
            }

            return StatusCode((int)response.StatusCode, "Failed to update location.");
        }

        // DELETE /api/Admin/location/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid location ID.");
            }

            var client = _http.CreateClient("LocationService");
            var response = await client.DeleteAsync($"api/Location/{id}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent(); // 204 No Content
            }

            return StatusCode((int)response.StatusCode, "Failed to delete location.");
        }
    }
}
