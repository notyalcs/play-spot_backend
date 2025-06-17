using System.Net.Http.Json;

using PlaySpotApi.Tests;

public class AdminTests : IClassFixture<AdminWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AdminTests(AdminWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // Test to check if POST /api/Admin/Location endpoint works correctly
    [Fact]
    public async Task PostLocation_ReturnsCreated()
    {
        // Arrange
        var location = new Admin.Api.DTOs.LocationDTO(
            0,
            "Test Location",
            "123 Test St",
            12.345678,
            98.765432
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/Admin/Location", location);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}
