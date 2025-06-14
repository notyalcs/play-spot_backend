using System.ComponentModel.DataAnnotations;
using Fullness.Api.Enums;

namespace Fullness.Api.Queries
{
    public class FullnessQuery
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        [Range(0, 4)]
        [EnumDataType(typeof(FullnessLevel))]
        public required FullnessLevel FullnessLevel { get; set; }
    }
}