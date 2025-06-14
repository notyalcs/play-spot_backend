namespace Sport.Api.Models
{
    public class Sport
    {
        public int SportId { get; set; }
        public int LocationId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
