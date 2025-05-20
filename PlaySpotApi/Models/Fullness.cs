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

    public class Fullness
    {
        public int FullnessId { get; set; }
        public DateTime DateTime { get; set; } // Date and time of the activity
        public FullnessLevel FullnessLevel { get; set; } // Fullness level of the location during the activity

        public required Location Location { get; set; } // Navigation property to the Location entity
    }
}
