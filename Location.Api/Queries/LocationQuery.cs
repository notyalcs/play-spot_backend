using System.ComponentModel.DataAnnotations;

namespace Location.Api.Queries
{
    public class LocationQuery
    {
        [Range(-90, 90)]
        public double? Latitude { get; set; }

        [Range(-180, 180)]
        public double? Longitude { get; set; }

        [Range(0, 100)]
        public double? Radius { get; set; } = 10;
    }
}
