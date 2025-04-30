namespace PlaySpotApi.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Coordinates { get; set; } // Coordinates in the format "latitude,longitude"

        public ICollection<LocationSport> LocationSports { get; set; } = new List<LocationSport>();
        public ICollection<LocationActivity> LocationActivities { get; set; } = new List<LocationActivity>();
    }
}
