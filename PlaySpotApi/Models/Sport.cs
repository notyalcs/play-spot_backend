namespace PlaySpotApi.Models
{
    public class Sport
    {
        public int SportId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; } // URL to an image representing the sport

        public ICollection<LocationSport>? LocationSports { get; set; } // List of locations where this sport is played
    }
}
