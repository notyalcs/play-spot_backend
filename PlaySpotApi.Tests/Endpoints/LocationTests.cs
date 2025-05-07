using System.Net.Http.Json;

using PlaySpotApi.Tests;
using PlaySpotApi.Models;

public class LocationTests: IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LocationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetLocations_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/locations/");

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<Location>>();
        Assert.NotNull(locations);
        Assert.NotEmpty(locations);
    }
}
