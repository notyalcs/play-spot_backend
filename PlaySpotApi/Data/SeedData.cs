using PlaySpotApi.Models;

namespace PlaySpotApi.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(PlaySpotDbContext context)
        {
            if (context.Locations.Any() || context.Sports.Any())
            {
                return; // DB has been seeded
            }

            var sports = new List<Sport>
            {
                new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Sport { Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                new Sport { Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
                new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };

            context.Sports.AddRange(sports);
            context.SaveChanges();

            var sportsDict = context.Sports.ToDictionary(s => s.Name, s => s);

            // Locations (with their Sports assigned)
            var bcit = new Location
            {
                Name = "BCIT Recreation Services",
                Address = "3700 Willingdon Ave SE16, Burnaby, BC V5G 3H2",
                // Coordinates = "49.24883642073071, -123.00085365852959",
                Latitude = 49.24883642073071,
                Longitude = -123.00085365852959,
                Sports = new List<Sport>
                {
                    sportsDict["Tennis"],
                    sportsDict["Table Tennis"],
                    sportsDict["Soccer"],
                    sportsDict["Basketball"]
                },
                Fullness = new List<Fullness> { }
            };

            var burnabyLake = new Location
            {
                Name = "Burnaby Lake Sports Complex West",
                Address = "3677 Kensington Ave, Burnaby, BC V5B 4Z6",
                // Coordinates = "49.25315305592485, -122.96961053016025",
                Latitude = 49.25315305592485,
                Longitude = -122.96961053016025,
                Sports = new List<Sport>
                {
                    sportsDict["Tennis"],
                    sportsDict["Soccer"],
                    sportsDict["Basketball"]
                },
                Fullness = new List<Fullness> { }
            };

            var broadview = new Location
            {
                Name = "Broadview Park",
                Address = "3955 Canada Wy, Burnaby, BC V5G 1C3",
                // Coordinates = "49.25552736455759, -123.01618148991702",
                Latitude = 49.25552736455759,
                Longitude = -123.01618148991702,
                Sports = new List<Sport>
                {
                    sportsDict["Tennis"],
                    sportsDict["Basketball"]
                },
                Fullness = new List<Fullness> { }
            };

            var centralPark = new Location
            {
                Name = "Central Park",
                Address = "3883 Imperial St, Burnaby, BC V5J 1A3",
                // Coordinates = "49.227936228186806, -123.01799296037849",
                Latitude = 49.227936228186806,
                Longitude = -123.01799296037849,
                Sports = new List<Sport>
                {
                    sportsDict["Tennis"],
                    sportsDict["Table Tennis"],
                    sportsDict["Basketball"]
                },
                Fullness = new List<Fullness> { }
            };

            var terryFoxPark = new Location
            {
                Name = "Terry Fox Park",
                Address = "1269 Riverside Dr, Port Coquitlam, BC V3B 7V7",
                Latitude = 49.2665,
                Longitude = -122.7352,
                Sports = new List<Sport>
    {
        sportsDict["Tennis"],
        sportsDict["Soccer"]
    },
                Fullness = new List<Fullness> { }
            };

            var portCoquitlamCommunityCentre = new Location
            {
                Name = "Port Coquitlam Community Centre",
                Address = "2150 Wilson Ave, Port Coquitlam, BC V3C 6J5",
                Latitude = 49.2620,
                Longitude = -122.7787,
                Sports = new List<Sport>
    {
        sportsDict["Basketball"],
        sportsDict["Table Tennis"]
    },
                Fullness = new List<Fullness> { }
            };

            var hydeCreekRecreationCentre = new Location
            {
                Name = "Hyde Creek Recreation Centre",
                Address = "1379 Laurier Ave, Port Coquitlam, BC V3B 2B9",
                Latitude = 49.2648,
                Longitude = -122.7653,
                Sports = new List<Sport>
    {
           sportsDict["Basketball"]
    },
                Fullness = new List<Fullness> { }
            };

            var gatesPark = new Location
            {
                Name = "Gates Park",
                Address = "2300 Reeve St, Port Coquitlam, BC V3C 6G1",
                Latitude = 49.2611,
                Longitude = -122.7775,
                Sports = new List<Sport>
    {
        sportsDict["Soccer"],
        sportsDict["Tennis"]
    },
                Fullness = new List<Fullness> { }
            };

            var routleyPark = new Location
            {
                Name = "Routley Park",
                Address = "1570 Western Dr, Port Coquitlam, BC V3C 2X1",
                Latitude = 49.2672,
                Longitude = -122.7584,
                Sports = new List<Sport>
    {
        sportsDict["Tennis"],
           sportsDict["Basketball"]
    },
                Fullness = new List<Fullness> { }
            };


            context.Locations.AddRange(bcit, burnabyLake, broadview, centralPark, terryFoxPark, routleyPark, gatesPark, hydeCreekRecreationCentre, portCoquitlamCommunityCentre);
            context.SaveChanges();
        }
    }
}
