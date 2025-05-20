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

    //this test returns the location that are within a given radius 
    [Fact]
    public async Task GetLocationsBySportAndRadius_ReturnsFilteredLocations()
    {
        // Arrange
        var sportName = "Table Tennis";
        // var coordinates = "49.24889950936577, -123.00379792501597"; // e.g., Vancouver downtown
        var latitude = 49.252339;
        var longitude = -122.987064;
        var radius = 2.0; // in kilometers

        var url = $"/locations?sportName={sportName}&latitude={latitude}&longitude={longitude}&radius={radius}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<object>>();

        Assert.NotNull(locations);
        Assert.NotEmpty(locations);
    }
        
    [Fact]
    public async Task GetLocations_WithSportName_ReturnsFilteredLocations()
    {
        // Arrange
        var sportName = "Soccer";

        // Act
        var response = await _client.GetAsync($"/locations?sportName={sportName}");

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
        var response = await _client.GetAsync($"/locations?latitude={latitude}&longitude={longitude}");

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<Location>>();
        Assert.NotNull(locations);
    }
}
