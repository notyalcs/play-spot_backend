using System.Diagnostics;
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

            // Sports
            // var tennis = new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." };
            // var tableTennis = new Sport { Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." };
            // var soccer = new Sport { Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." };
            // var basketball = new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." };

            // Locations (with their Sports assigned)
            var bcit = new Location
            {
                Name = "BCIT Recreation Services",
                Address = "3700 Willingdon Ave SE16, Burnaby, BC V5G 3H2",
                Coordinates = "49.24883642073071, -123.00085365852959",
                Sports = new List<Sport> 
                { 
                    new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                    new Sport { Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                    new Sport { Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
                    new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
                },
                LocationActivities = new List<LocationActivity>
                {
                    new LocationActivity { DateTime = new DateTime(2025, 5, 1, 1, 30, 0, DateTimeKind.Utc), FullnessLevel = FullnessLevel.Available },
                    new LocationActivity { DateTime = new DateTime(2025, 5, 1, 0, 30, 0, DateTimeKind.Utc), FullnessLevel = FullnessLevel.Moderate }
                }
            };

            var burnabyLake = new Location
            {
                Name = "Burnaby Lake Sports Complex West",
                Address = "3677 Kensington Ave, Burnaby, BC V5B 4Z6",
                Coordinates = "49.25315305592485, -122.96961053016025",
                Sports = new List<Sport> 
                { 
                    new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                    new Sport { Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
                    new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
                },
                LocationActivities = new List<LocationActivity>
                {
                    new LocationActivity { DateTime = new DateTime(2025, 5, 1, 1, 30, 0, DateTimeKind.Utc), FullnessLevel = FullnessLevel.Crowded }
                }
            };

            var broadview = new Location
            {
                Name = "Broadview Park",
                Address = "3955 Canada Wy, Burnaby, BC V5G 1C3",
                Coordinates = "49.25552736455759, -123.01618148991702",
                Sports = new List<Sport> 
                {
                    new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                    new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
                },
                LocationActivities = new List<LocationActivity>
                {
                    new LocationActivity { DateTime = new DateTime(2025, 5, 1, 0, 45, 0, DateTimeKind.Utc), FullnessLevel = FullnessLevel.Full }
                }
            };

            var centralPark = new Location
            {
                Name = "Central Park",
                Address = "3883 Imperial St, Burnaby, BC V5J 1A3",
                Coordinates = "49.227936228186806, -123.01799296037849",
                Sports = new List<Sport> 
                {
                    new Sport { Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                    new Sport { Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                    new Sport { Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
                },
                LocationActivities = new List<LocationActivity>
                {
                    new LocationActivity { DateTime = new DateTime(2025, 5, 1, 1, 0, 0, DateTimeKind.Utc), FullnessLevel = FullnessLevel.Closed }
                }
            };
            
            context.Locations.AddRange(bcit, burnabyLake, broadview, centralPark);
            context.SaveChanges();
        }
    }
}
