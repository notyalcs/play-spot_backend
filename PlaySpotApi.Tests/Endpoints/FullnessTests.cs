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
}
