using Microsoft.AspNetCore.Mvc;

using Aggregator.Api.DTOs;
using System.Data;

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
            var location = await locationClient.GetFromJsonAsync<LocationDTO>($"api/Location/{id}");
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            var sportClient = _http.CreateClient("SportService");
            var sports = await sportClient.GetFromJsonAsync<List<SportDTO>>($"api/Sport?locationId={id}");

            // var fullnessClient = _http.CreateClient("FullnessService");
            // var fullnessList = await fullnessClient.GetFromJsonAsync<List<FullnessDTO>>($"api/Fullness?locationId={id}");

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

            Console.WriteLine($"Fetching locations from: {locationClient.BaseAddress}");

            var locations = await locationClient.GetFromJsonAsync<List<LocationDTO>>("api/Location");
            if (locations == null || !locations.Any())
            {
                return NotFound("No locations found.");
            }

            var sportClient = _http.CreateClient("SportService");
            var fullnessClient = _http.CreateClient("FullnessService");

            var result = new List<CompositeLocationSportDTO>();

            var now = DateTime.UtcNow;
            // var cutoffTime = now.AddHours(-2);

            foreach (var location in locations)
            {
                Console.WriteLine($"Processing location: {location.Name} ({location.LocationId})");

                var recentFullnessRecords = await fullnessClient
                    .GetFromJsonAsync<List<FullnessDTO>>($"api/Fullness/recent?locationId={location.LocationId}")
                    ?? new List<FullnessDTO>();

                var scaledScore = CalculateFullnessScore(recentFullnessRecords, now); //(int)Math.Round(averageFullness / 4.0 * 100);

                Console.WriteLine($"Scaled fullness score for {location.Name}: {scaledScore}");

                var sports = await sportClient
                    .GetFromJsonAsync<List<SportDTO>>($"api/Sport?locationId={location.LocationId}")
                    ?? new List<SportDTO>();

                if (string.IsNullOrEmpty(sportName))
                {
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
                    continue; // No sport filter, add all locations
                }

                if (!sports.Any(s => s.Name.Equals(sportName, StringComparison.OrdinalIgnoreCase)))
                {
                    continue; // Skip this location if no sports match the filter
                }

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
        
        private int CalculateFullnessScore(List<FullnessDTO> fullnessRecords, DateTime now)
        {
            if (fullnessRecords == null || !fullnessRecords.Any())
            {
                Console.WriteLine("No fullness records available.");
                return 0;
            }

            double weightedSum = 0;
            double weightTotal = 0;

            foreach (var f in fullnessRecords)
            {
                var minutesSince = (now - f.TimeStamp).TotalMinutes;
                var weight = 120 - minutesSince; // Weight decreases with time
                if (weight <= 0)
                {
                    continue; // Skip if weight is non-positive
                }

                weightedSum += f.FullnessLevel / 4.0 * weight;
                weightTotal += weight;

                Console.WriteLine($"Fullness Record: {f.FullnessLevel}, Time: {f.TimeStamp}, Weight: {weight}");
                Console.WriteLine($"Weighted Sum: {weightedSum}, Weight Total: {weightTotal}");
            }

            return (int)Math.Round(weightTotal > 0 ? (weightedSum / weightTotal * 100) : 0);
        }
    }
}
