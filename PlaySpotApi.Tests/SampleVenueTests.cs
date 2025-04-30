using PlaySpotApi.Models;

namespace PlaySpotApi.Tests;

public class SampleVenueTests
{
    [Fact]
    public void CanCreateLocation()
    {
        var location = new Location
        {
            LocationId = 0,
            Name = "Sample Venue",
            Address = "123 Sample St, Sample City, SC 12345",
            Coordinates = "34.0522,-118.2437"
        }; 

        Assert.Equal(0, location.LocationId);
    }
}
