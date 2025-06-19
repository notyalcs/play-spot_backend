using System.Diagnostics;
using System.Net.Http.Json;

using PlaySpotApi.Tests;

public class FullnessTests : IClassFixture<FullnessWebApplicationFactory>
{
    private readonly HttpClient _client;

    public FullnessTests(FullnessWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // Test to check if POST /api/Fullness endpoint works correctly
    [Fact]
    public async Task PostFullness_ReturnsCreated()
    {
        // Arrange
        var fullness = new Fullness.Api.Models.Fullness
        {
            LocationId = 1,
            FullnessLevel = Fullness.Api.Enums.FullnessLevel.Crowded,
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Fullness", fullness);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }

    // [Fact]
    // public async Task PostFullness_MissingLocationId_ReturnsBadRequest()
    // {
    //     // Arrange
    //     var fullness = new Fullness.Api.Models.Fullness
    //     {
    //         LocationId = 0, // Invalid
    //         FullnessLevel = Fullness.Api.Enums.FullnessLevel.Moderate
    //     };

    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/Fullness", fullness);

    //     // Assert
    //     Assert.False(response.IsSuccessStatusCode);
    //     Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    // }
    
    // testing 100 updating fullness request
    [Fact]
    public async Task PostFullness_HighLoad_ResponseTimeAcceptable()
    {
        const int requestCount = 100;
        var stopwatch = Stopwatch.StartNew();

        var tasks = Enumerable.Range(0, requestCount).Select(async i =>
        {
            var payload = new Fullness.Api.Models.Fullness
            {
                LocationId = 1,
                FullnessLevel = Fullness.Api.Enums.FullnessLevel.Crowded
            };

            var response = await _client.PostAsJsonAsync("/api/Fullness", payload);
            response.EnsureSuccessStatusCode();
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        var avgTimePerRequest = stopwatch.Elapsed.TotalMilliseconds / requestCount;
        Assert.True(avgTimePerRequest < 500, $"Average POST response time too high: {avgTimePerRequest}ms");
    }
}
