using System.Net.Http.Json;

using PlaySpotApi.Tests;

public class AggregatorTests : IClassFixture<AggregatorWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AggregatorTests(AggregatorWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // Test to check if Get /api/Composite endpoint works correctly
    [Fact]
    public async Task GetComposite_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/Composite/locations");

        // Assert
        response.EnsureSuccessStatusCode();
        var compositeData = await response.Content.ReadFromJsonAsync<List<Aggregator.Api.DTOs.CompositeLocationSportDTO>>();
        Assert.NotNull(compositeData);
        Assert.NotEmpty(compositeData);
    }
}
