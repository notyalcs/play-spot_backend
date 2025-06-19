using PlaySpotApi.Models;

namespace PlaySpotApi.DTOs
{
    public class LocationDTO
    {
        public int LocationId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required ICollection<Sport> Sports { get; set; }
        public int FullnessScore { get; set; } // 0 to 100
        public double Distance { get; set; } // in kilometers
    }
}
