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

    //this test returns the location that are within a given radius 
    [Fact]
    public async Task GetLocationsBySportAndRadius_ReturnsFilteredLocations()
    {
        // Arrange
        string sportName = "Basketball";
        string coordinates = "49.24889950936577, -123.00379792501597"; // e.g., Vancouver downtown
        double radius = 10.0; // in kilometers

        var url = $"/locations/locations-by-sport?sportName={sportName}&coordinates={coordinates}&radius={radius}";


        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<object>>();

        Assert.NotNull(locations);
        Assert.NotEmpty(locations);
    }
}
