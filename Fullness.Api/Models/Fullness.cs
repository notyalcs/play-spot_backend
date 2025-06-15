using Fullness.Api.Enums;

namespace Fullness.Api.Models
{
    public class Fullness
    {
        public int FullnessId { get; set; }
        public required int LocationId { get; set; }

        public DateTime TimeStamp { get; set; }
        public FullnessLevel FullnessLevel { get; set; }
    }
}