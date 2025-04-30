namespace PlaySpotApi.Models
{
    public class LocationSport
    {
        public int LocationId { get; set; }
        public Location? Location { get; set; } // Navigation property to the Location entity

        public int SportId { get; set; }
        public Sport? Sport { get; set; } // Navigation property to the Sport entity
    }
}
