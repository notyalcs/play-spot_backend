namespace Aggregator.Api.DTOs
{
    public record FullnessDTO(
        int FullnessId,
        int LocationId,
        DateTime TimeStamp,
        int FullnessLevel
    );
}
