namespace Location.Api.DTOs
{
    public record LocationDTO(
        int LocationId,
        string Name,
        string Address,
        double Latitude,
        double Longitude
    );
}
