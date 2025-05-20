using System.Net.Http.Json;

using PlaySpotApi.Tests;
using PlaySpotApi.Models;

public class SportTests: IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SportTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetSports_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/sports/");

        // Assert
        response.EnsureSuccessStatusCode();
        var sports = await response.Content.ReadFromJsonAsync<List<string>>();
        Assert.NotNull(sports);
        Assert.NotEmpty(sports);
    }
}
