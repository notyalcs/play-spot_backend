using Sport.Api.Models;

namespace Sport.Api.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(SportDbContext context)
        {
            if (context.Sports.Any())
            {
                return;
            }

            var sportsBcit = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 1, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 1, Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                new Models.Sport {LocationId = 1, Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
                new Models.Sport {LocationId = 1, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsBcit);

            var sportsBurnabyLake = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 2, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 2, Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
                new Models.Sport {LocationId = 2, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsBurnabyLake);

            var sportsBroadview = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 3, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 3, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsBroadview);

            var sportsCentralPark = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 4, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 4, Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                new Models.Sport {LocationId = 4, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsCentralPark);

            var sportsTerryFoxPark = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 5, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 5, Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
            };
            context.Sports.AddRange(sportsTerryFoxPark);

            var sportsPocoCoumminutyCenter = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 6, Name = "Table Tennis", Description = "A sport in which two or four players hit a lightweight ball, also known as a ping-pong ball, back and forth across a table using small solid rackets." },
                new Models.Sport {LocationId = 6, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsPocoCoumminutyCenter);

            var sportsHydeCreekRecreationCenter = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 7, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsHydeCreekRecreationCenter);

            var sportsGatesPark = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 8, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 8, Name = "Soccer", Description = "A team sport played with a spherical ball between two teams of 11 players." },
            };
            context.Sports.AddRange(sportsGatesPark);

            var sportsRoutleyPark = new List<Models.Sport>
            {
                new Models.Sport {LocationId = 9, Name = "Tennis", Description = "A racket sport that can be played individually against a single opponent or between two teams of two players each." },
                new Models.Sport {LocationId = 9, Name = "Basketball", Description = "A team sport in which two teams, most commonly of five players each, opposing one another on a rectangular court." }
            };
            context.Sports.AddRange(sportsRoutleyPark);

            context.SaveChanges();
        }
    }
}
