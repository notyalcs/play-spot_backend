using PlaySpotApi.Models;

namespace PlaySpotApi.Tests;

public class SampleVenueTests
{
    [Fact]
    public void CanCreateVenueItem()
    {
        var item = new VenueItem
        {
            Id = 0,
            Name = "Sample Venue",
            Address = "123 Sample St, Sample City, SC 12345",
            Sports = new List<string> { "Soccer", "Basketball" }
        };

        Assert.Equal(0, item.Id);
    }
}
