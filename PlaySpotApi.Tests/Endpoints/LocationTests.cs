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
        var response = await _client.GetAsync("/locations");

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<Location>>();
        Assert.NotNull(locations);
        Assert.NotEmpty(locations);
    }

    [Fact]
    public async Task GetLocations_WithSportName_ReturnsFilteredLocations()
    {
        // Arrange
        var sportName = "Soccer";

        // Act
        var response = await _client.GetAsync($"/locations/?sportName={sportName}");

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<Location>>();
        Assert.NotNull(locations);
        Assert.All(locations, l => Assert.Contains(sportName, l.Sports.Select(s => s.Name)));
    }

    [Fact]
    public async Task GetLocations_WithLatitudeLongitude_ReturnsFilteredLocations()
    {
        // Arrange
        var latitude =  49.24883642073071;
        var longitude = -123.00085365852959;

        // Act
        var response = await _client.GetAsync($"/locations/?latitude={latitude}&longitude={longitude}");

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<Location>>();
        Assert.NotNull(locations);
        Assert.All(locations, l => Assert.Equal(latitude, l.Latitude));
        Assert.All(locations, l => Assert.Equal(longitude, l.Longitude));
    }
}
