namespace PlaySpotApi.Models
{
    // THIS IS A TEST ITEM. FEEL FREE TO CHANGE IT
    public class VenueItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<string>? Sports { get; set; } // TODO: Change this to hold a list of SportItems
    }
}
