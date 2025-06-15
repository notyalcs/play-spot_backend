namespace Aggregator.Api.DTOs
{
    public record CompositeLocationSportDTO(
        int LocationId,
        string Name,
        string Address,
        double Latitude,
        double Longitude,
        List<SportDTO> Sports,
        List<string> LocationActivities,
        double? FullnessScore
    );
}
