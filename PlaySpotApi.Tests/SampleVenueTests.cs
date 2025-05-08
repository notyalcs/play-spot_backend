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
            Latitude = 34.0522,
            Longitude = -118.2437
        }; 

        Assert.Equal(0, location.LocationId);
    }

    [Fact]
    public void CanCreateLocationActivity()
    {
        var locationActivity = new LocationActivity
        {
            LocationActivityId = 0,
            LocationId = 1,
            DateTime = DateTime.Now,
            FullnessLevel = FullnessLevel.Available
        };

        Assert.Equal(0, locationActivity.LocationActivityId);
    }
}
