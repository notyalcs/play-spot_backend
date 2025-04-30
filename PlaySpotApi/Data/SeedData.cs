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

            var tennis = new Sport { SportId = 1, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." };
            var tableTennis = new Sport { SportId = 2, Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." };
            var soccer = new Sport { SportId = 3, Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." };
            var basketball = new Sport { SportId = 4, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." };

            var bcit = new Location { LocationId = 1, Name = "BCIT Recreation Services", Address = "3700 Willingdon Ave SE16, Burnaby, BC V5G 3H2", Coordinates = "49.24883642073071, -123.00085365852959" };
            var burnabyLake = new Location { LocationId = 2, Name = "Burnaby Lake Sports Complex West", Address = "3677 Kensington Ave, Burnaby, BC V5B 4Z6", Coordinates = "49.25315305592485, -122.96961053016025" };
            var broadview = new Location { LocationId = 3, Name = "Broadview Park", Address = "3955 Canada Wy, Burnaby, BC V5G 1C3", Coordinates = "49.25552736455759, -123.01618148991702" };
            var centralPark = new Location { LocationId = 4, Name = "Central Park", Address = "3883 Imperial St, Burnaby, BC V5J 1A3", Coordinates = "49.227936228186806, -123.01799296037849" };

            var locationSport1 = new LocationSport { LocationId = 1, Location = bcit, SportId = 1, Sport = tennis };
            var locationSport2 = new LocationSport { LocationId = 1, Location = bcit, SportId = 2, Sport = tableTennis };
            var locationSport3 = new LocationSport { LocationId = 1, Location = bcit, SportId = 3, Sport = soccer };
            var locationSport4 = new LocationSport { LocationId = 1, Location = bcit, SportId = 4, Sport = basketball };
            var locationSport5 = new LocationSport { LocationId = 2, Location = burnabyLake, SportId = 1, Sport = tennis };
            var locationSport6 = new LocationSport { LocationId = 2, Location = burnabyLake, SportId = 3, Sport = soccer };
            var locationSport7 = new LocationSport { LocationId = 3, Location = broadview, SportId = 1, Sport = tennis };
            var locationSport8 = new LocationSport { LocationId = 3, Location = broadview, SportId = 4, Sport = basketball };
            var locationSport9 = new LocationSport { LocationId = 4, Location = centralPark, SportId = 1, Sport = tennis };
            var locationSport10 = new LocationSport { LocationId = 4, Location = centralPark, SportId = 2, Sport = tableTennis };
            var locationSport11 = new LocationSport { LocationId = 4, Location = centralPark, SportId = 4, Sport = basketball };

            var locationActivity1 = new LocationActivity { LocationActivityId = 1, LocationId = 1, Location = bcit, DateTime = DateTime.SpecifyKind(new DateTime(2025, 05, 1, 1, 30, 0), DateTimeKind.Utc), FullnessLevel = FullnessLevel.Available };
            var locationActivity2 = new LocationActivity { LocationActivityId = 2, LocationId = 1, Location = bcit, DateTime = DateTime.SpecifyKind(new DateTime(2025, 05, 1, 0, 30, 0), DateTimeKind.Utc), FullnessLevel = FullnessLevel.Moderate };
            var locationActivity3 = new LocationActivity { LocationActivityId = 3, LocationId = 2, Location = burnabyLake, DateTime = DateTime.SpecifyKind(new DateTime(2025, 05, 1, 1, 30, 0), DateTimeKind.Utc), FullnessLevel = FullnessLevel.Crowded };
            var locationActivity4 = new LocationActivity { LocationActivityId = 4, LocationId = 3, Location = broadview, DateTime = DateTime.SpecifyKind(new DateTime(2025, 05, 1, 0, 45, 0), DateTimeKind.Utc), FullnessLevel = FullnessLevel.Full };
            var locationActivity5 = new LocationActivity { LocationActivityId = 5, LocationId = 4, Location = centralPark, DateTime = DateTime.SpecifyKind(new DateTime(2025, 05, 1, 1, 0, 0), DateTimeKind.Utc), FullnessLevel = FullnessLevel.Closed };

            // bcit.LocationSports.Add(locationSport1);
            // bcit.LocationSports.Add(locationSport2);
            // bcit.LocationSports.Add(locationSport3);
            // bcit.LocationSports.Add(locationSport4);
            bcit.LocationSports = new List<LocationSport> { locationSport1, locationSport2, locationSport3, locationSport4 };
            // burnabyLake.LocationSports.Add(locationSport5);
            // burnabyLake.LocationSports.Add(locationSport6);
            burnabyLake.LocationSports = new List<LocationSport> { locationSport5, locationSport6 };
            // broadview.LocationSports.Add(locationSport7);
            // broadview.LocationSports.Add(locationSport8);
            broadview.LocationSports = new List<LocationSport> { locationSport7, locationSport8 };
            // centralPark.LocationSports.Add(locationSport9);
            // centralPark.LocationSports.Add(locationSport10);
            // centralPark.LocationSports.Add(locationSport11);
            centralPark.LocationSports = new List<LocationSport> { locationSport9, locationSport10, locationSport11 };

            // bcit.LocationActivities.Add(locationActivity1);
            // bcit.LocationActivities.Add(locationActivity2);
            bcit.LocationActivities = new List<LocationActivity> { locationActivity1, locationActivity2 };
            // burnabyLake.LocationActivities.Add(locationActivity3);
            burnabyLake.LocationActivities = new List<LocationActivity> { locationActivity3 };
            // broadview.LocationActivities.Add(locationActivity4);
            broadview.LocationActivities = new List<LocationActivity> { locationActivity4 };
            // centralPark.LocationActivities.Add(locationActivity5);
            centralPark.LocationActivities = new List<LocationActivity> { locationActivity5 };

            // tennis.LocationSports.Add(locationSport1);
            // tennis.LocationSports.Add(locationSport5);
            // tennis.LocationSports.Add(locationSport7);
            // tennis.LocationSports.Add(locationSport9);
            // tennis.LocationSports = new List<LocationSport> { locationSport1, locationSport5, locationSport7, locationSport9 };
            // tableTennis.LocationSports.Add(locationSport2);
            // tableTennis.LocationSports.Add(locationSport10);
            // tableTennis.LocationSports = new List<LocationSport> { locationSport2, locationSport10 };
            // soccer.LocationSports.Add(locationSport3);
            // soccer.LocationSports.Add(locationSport6);
            // soccer.LocationSports = new List<LocationSport> { locationSport3, locationSport6 };
            // basketball.LocationSports.Add(locationSport4);
            // basketball.LocationSports.Add(locationSport8);
            // basketball.LocationSports.Add(locationSport11);
            // basketball.LocationSports = new List<LocationSport> { locationSport4, locationSport8, locationSport11 };

            context.Sports.AddRange(tennis, tableTennis, soccer, basketball);
            context.Locations.AddRange(bcit, burnabyLake, broadview, centralPark);
            context.LocationSports.AddRange(locationSport1, locationSport2, locationSport3, locationSport4, locationSport5, locationSport6, locationSport7, locationSport8, locationSport9, locationSport10, locationSport11);
            context.LocationActivities.AddRange(locationActivity1, locationActivity2, locationActivity3, locationActivity4, locationActivity5);

            context.SaveChanges();
        }
    }
}
