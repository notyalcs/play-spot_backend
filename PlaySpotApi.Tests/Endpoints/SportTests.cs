using System.Net.Http.Json;

using PlaySpotApi.Tests;

public class SportTests : IClassFixture<SportWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SportTests(SportWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetSports_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/Sport");

        // Assert
        response.EnsureSuccessStatusCode();
        var sports = await response.Content.ReadFromJsonAsync<List<Sport.Api.DTOs.SportDTO>>();
        Assert.NotNull(sports);
        Assert.NotEmpty(sports);
    }
}
