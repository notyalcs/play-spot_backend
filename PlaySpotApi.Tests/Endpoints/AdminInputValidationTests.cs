using System.Net;
using System.Net.Http.Json;
using PlaySpotApi.Tests;
using Admin.Api.DTOs;

public class AdminValidationTests : IClassFixture<AdminWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AdminValidationTests(AdminWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostLocation_WithMissingNameAndCoordinates_ReturnsBadRequest()
    {
        // Arrange: Missing Name, Latitude, Longitude
        var invalidLocation = new
        {
            Id = 0,
            Address = "123 Fake Street"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Admin/Location", invalidLocation);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // [Fact]
    // public async Task PostLocation_DuplicateName_ReturnsConflict()
    // {
    //     // Arrange: First post with unique name
    //     var location = new LocationDTO(
    //         LocationId: 0,
    //         Name: "Duplicate Test Location",
    //         Address: "123 Main St",
    //         Latitude: 49.25,
    //         Longitude: -123.00
    //     );

    //     var response1 = await _client.PostAsJsonAsync("/api/Admin/Location", location);
    //     response1.EnsureSuccessStatusCode();

    //     // Act: Post again with same name and address
    //     var response2 = await _client.PostAsJsonAsync("/api/Admin/Location", location);

    //     // Assert: Assuming backend prevents duplicates with 409 Conflict
    //     Assert.Equal(HttpStatusCode.Conflict, response2.StatusCode);
    // }
}
