using Microsoft.AspNetCore.Mvc;

using Aggregator.Api.DTOs;

namespace Aggregator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompositeController : ControllerBase
    {
        private readonly IHttpClientFactory _http;

        public CompositeController(IHttpClientFactory http) => _http = http;

        [HttpGet("location/{id}/details")]
        public async Task<IActionResult> GetLocationDetails(int id)
        {
            var locationClient = _http.CreateClient("LocationService");
            var location = await locationClient.GetFromJsonAsync<LocationDTO>($"/{id}");
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            var sportClient = _http.CreateClient("SportService");
            var sports = await sportClient.GetFromJsonAsync<List<SportDTO>>($"?locationId={id}");

            // var fullnessClient = _http.CreateClient("FullnessService");
            // var fullnessList = await fullnessClient.GetFromJsonAsync<List<FullnessDTO>>($"?locationId={id}");

            var composite = new
            {
                Location = location,
                Sports = sports ?? new List<SportDTO>(),
                // Fullness = fullnessList ?? new List<FullnessDTO>()
            };

            return Ok(composite);
        }

        // GET api/composite/locations?sportName={sportName}
        [HttpGet("locations")]
        public async Task<IActionResult> GetLocationsWithSports([FromQuery] string? sportName)
        {
            var locationClient = _http.CreateClient("LocationService");
            var locations = await locationClient.GetFromJsonAsync<List<LocationDTO>>("/");
            if (locations == null || !locations.Any())
            {
                return NotFound("No locations found.");
            }
            
            var sportClient = _http.CreateClient("SportService");
            var fullnessClient = _http.CreateClient("FullnessService");

            var result = new List<CompositeLocationSportDTO>();

            var now = DateTime.UtcNow;
            var cutoffTime = now.AddHours(-2);

            foreach (var location in locations)
            {
                var sports = await sportClient
                    .GetFromJsonAsync<List<SportDTO>>($"?locationId={location.LocationId}")
                    ?? new List<SportDTO>();

                if (string.IsNullOrEmpty(sportName) &&
                    !sports.Any(s => s.Name.Equals(sportName, StringComparison.OrdinalIgnoreCase)))
                {
                    continue; // Skip this location if no sports match the filter
                }

                var fullnessRecords = await fullnessClient
                    .GetFromJsonAsync<List<FullnessDTO>>($"?locationId={location.LocationId}")
                    ?? new List<FullnessDTO>();

                var recent = fullnessRecords
                    .Where(f => f.TimeStamp >= cutoffTime)
                    .OrderByDescending(f => f.TimeStamp)
                    .ToList();

                double weightedSum = 0;
                double weightTotal = 0;
                foreach (var f in recent)
                {
                    var minutesSince = (now - f.TimeStamp).TotalMinutes;
                    var weight = 120 - minutesSince;
                    if (weight <= 0)
                    {
                        continue; // Skip if weight is non-positive
                    }

                    weightedSum += (int)f.FullnessLevel * weight;
                    weightTotal += weight;
                }

                var averageFullness = weightTotal > 0
                    ? weightedSum / weightTotal
                    : 0;
                var scaledScore = (int)Math.Round(averageFullness / 4.0 * 100);

                result.Add(new CompositeLocationSportDTO(
                    location.LocationId,
                    location.Name,
                    location.Address,
                    location.Latitude,
                    location.Longitude,
                    sports,
                    new List<string>(), // Placeholder for activities
                    scaledScore
                ));
            }

            return Ok(result);
        }
    }
}
