namespace PlaySpotApi.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public ICollection<Sport> Sports { get; set; } = new List<Sport>();
        public ICollection<LocationActivity> LocationActivities { get; set; } = new List<LocationActivity>();
    }
}
