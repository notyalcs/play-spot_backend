using Location.Api.Models;

namespace Location.Api.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(LocationDbContext context)
        {
            if (context.Locations.Any())
            {
                return;
            }

            var bcit = new Models.Location
            {
                Name = "BCIT Recreation Services",
                Address = "3700 Willingdon Ave SE16, Burnaby, BC V5G 3H2",
                // Coordinates = "49.24883642073071, -123.00085365852959",
                Latitude = 49.24883642073071,
                Longitude = -123.00085365852959
            };

            var burnabyLake = new Models.Location
            {
                Name = "Burnaby Lake Sports Complex West",
                Address = "3677 Kensington Ave, Burnaby, BC V5B 4Z6",
                // Coordinates = "49.25315305592485, -122.96961053016025",
                Latitude = 49.25315305592485,
                Longitude = -122.96961053016025
            };

            var broadview = new Models.Location
            {
                Name = "Broadview Park",
                Address = "3955 Canada Wy, Burnaby, BC V5G 1C3",
                // Coordinates = "49.25552736455759, -123.01618148991702",
                Latitude = 49.25552736455759,
                Longitude = -123.01618148991702
            };

            var centralPark = new Models.Location
            {
                Name = "Central Park",
                Address = "3883 Imperial St, Burnaby, BC V5J 1A3",
                // Coordinates = "49.227936228186806, -123.01799296037849",
                Latitude = 49.227936228186806,
                Longitude = -123.01799296037849
            };

            var terryFoxPark = new Models.Location
            {
                Name = "Terry Fox Park",
                Address = "1269 Riverside Dr, Port Coquitlam, BC V3B 7V7",
                Latitude = 49.2665,
                Longitude = -122.7352
            };

            var portCoquitlamCommunityCentre = new Models.Location
            {
                Name = "Port Coquitlam Community Centre",
                Address = "2150 Wilson Ave, Port Coquitlam, BC V3C 6J5",
                Latitude = 49.2620,
                Longitude = -122.7787
            };

            var hydeCreekRecreationCentre = new Models.Location
            {
                Name = "Hyde Creek Recreation Centre",
                Address = "1379 Laurier Ave, Port Coquitlam, BC V3B 2B9",
                Latitude = 49.2648,
                Longitude = -122.7653
            };

            var gatesPark = new Models.Location
            {
                Name = "Gates Park",
                Address = "2300 Reeve St, Port Coquitlam, BC V3C 6G1",
                Latitude = 49.2611,
                Longitude = -122.7775
            };

            var routleyPark = new Models.Location
            {
                Name = "Routley Park",
                Address = "1570 Western Dr, Port Coquitlam, BC V3C 2X1",
                Latitude = 49.2672,
                Longitude = -122.7584
            };

            context.Locations.AddRange(
                bcit,
                burnabyLake,
                broadview,
                centralPark,
                terryFoxPark,
                portCoquitlamCommunityCentre,
                hydeCreekRecreationCentre,
                gatesPark,
                routleyPark
            );
            context.SaveChanges();
        }
    }
}