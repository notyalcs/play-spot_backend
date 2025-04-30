namespace PlaySpotApi.Models
{
    public enum FullnessLevel
    {
        Closed = 0,
        Available = 1,
        Moderate = 2,
        Crowded = 3,
        Full = 4
    }

    public class LocationActivity
    {
        public int LocationActivityId { get; set; }

        public int LocationId { get; set; }
        public Location? Location { get; set; } // Navigation property to the Location entity

        public DateTime dateTime { get; set; } // Date and time of the activity
        public FullnessLevel FullnessLevel { get; set; } // Fullness level of the location during the activity
    }
}
