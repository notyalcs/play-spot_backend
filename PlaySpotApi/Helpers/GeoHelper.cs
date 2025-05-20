namespace PlaySpotApi.Helpers
{
    public static class GeoHelper
    {
        private const double EarthRadiusKm = 6371;

        // Calculate the Haversine distance in kilometers
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            double a = Math.Sin(dLat/2) * Math.Sin(dLat/2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon/2) * Math.Sin(dLon/2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusKm * c;
        }

        private static double ToRadians(double deg) => deg * (Math.PI / 180);

        // Returns true if the two points are within the given radius (in km)
        public static bool IsWithinRadius(
            double originLat, double originLon,
            double targetLat, double targetLon,
            double radiusKm)
        {
            var distance = CalculateDistance(originLat, originLon, targetLat, targetLon);
            return distance <= radiusKm;
        }
    }
}
