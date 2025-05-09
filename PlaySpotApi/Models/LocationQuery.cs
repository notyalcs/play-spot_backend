using System.ComponentModel.DataAnnotations;

namespace PlaySpotApi.Models
{
    public class LocationQuery
    {
        public string? SportName { get; set; }

        [Range(-90, 90)]
        public double? Latitude { get; set; }

        [Range(-180, 180)]
        public double? Longitude { get; set; }

        [Range(0, 100)]
        public int? Radius { get; set; } = 10;
    }
}
