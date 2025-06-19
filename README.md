# play-spot_backend

## Development
### Prerequisites
- Dotnet 9.x
- Docker
### Instructions
- To START the database and dotnet app:
    - run `dev.sh` 
        - A bash terminal should open and it will tell you the port it is listening on
        - Port is probably 5102: 
            - `http://localhost:5102`
        - You can see API endpoints built from OpenAPI and Scalar at:
            - `http://localhost:5102/scalar/v1`
- To STOP the database and dotnet app:
    - OLD: ~~run `cleanup.sh`~~
        - ~~This will remove the docker container (not volume)~~
    - Just stop the `dev.sh` script (easiest is just `CTRL+C`)

### Notes
- The scripts aren't mandatory, you should be able to start things however you want, this was just convenient IMO.
- While developing, the script starts the dotnet app in `watch` mode which will have hot-reload features to not have to rebuild all the time.

## Committing
- [TODO] The main branch should be locked and require Pull Requests with approval
- Try your best to include at least 1 unit test on new features in the `PlaySpotApi.Tests` directory.
- GitHub Actions will run and should prevent broken states
    - To see how that works look in: `/.github/workflows/ci.yml`

## Approximate repo structure
```bash
play-spot_backend/
│
├── .github/
│   └── workflows/
│       └── ci.yml       # GitHub Actions workflow
│
├── db/
│   └── init/
│       └── 01-create-schemas.sql       # local DB setup
│
├── Admin.Api/
│   └── Controllers/
│       ├── LocationController.cs
│       ├── ...
│   └── DTOs/
│       ├── LocationDTO.cs
│       ├── ...
│   ├── Program.cs
│   ├── Admin.Api.csproj
│   ├── dockerfile
│   ├── ...
│
├── Aggregator.Api/
│   └── Controllers/
│       ├── CompositeController.cs
│       ├── ...
│   └── DTOs/
│       ├── CompositeLocationSportDTO.cs
│       ├── ...
│   ├── Program.cs
│   ├── Aggregator.Api.csproj
│   ├── dockerfile
│   ├── ...
│
├── Fullness.Api/
│   └── Controllers/
│       ├── FullnessController.cs
│       ├── ...
│   └── Data/
│       ├── FullnessDbContext.cs
│       ├── ...
│   └── Models/
│       ├── Fullness.cs
│       ├── ...
│   └── Queries/
│       ├── FullnessQuery.cs
│       ├── ...
│   ├── Program.cs
│   ├── Fullness.Api.csproj
│   ├── dockerfile
│   ├── ...
│
├── Location.Api/
│   └── Controllers/
│       ├── LocationController.cs
│       ├── ...
│   └── Data/
│       ├── LocationDbContext.cs
│       ├── ...
│   └── Models/
│       ├── Location.cs
│       ├── ...
│   └── Queries/
│       ├── LocationQuery.cs
│       ├── ...
│   └── DTOs/
│       ├── LocationDTO.cs
│       ├── ...
│   ├── Program.cs
│   ├── Location.Api.csproj
│   ├── dockerfile
│   ├── ...
│
├── Sport.Api/
│   └── Controllers/
│       ├── SportController.cs
│       ├── ...
│   └── Data/
│       ├── SportDbContext.cs
│       ├── ...
│   └── Models/
│       ├── Sport.cs
│       ├── ...
│   └── Queries/
│       ├── SportQuery.cs
│       ├── ...
│   └── DTOs/
│       ├── SportDTO.cs
│       ├── ...
│   ├── Program.cs
│   ├── Sport.Api.csproj
│   ├── dockerfile
│   ├── ...
│
├── PlaySpotApi.Tests/
│   ├── SampleVenueTests.cs                # Basic xUnit test
│   ├── PlaySpotApi.Tests.csproj
│
├── deprecated/PlaySpotApi (DEPRECATED)/
│   └── Data/
│       ├── PlaySpotDbContext.cs
│       ├── ...
│   └── Models/
│       ├── VenueItem.cs
│       ├── ...
│   └── Routes/
│       ├── LocationRoutes.cs
│       ├── ...
│   ├── Program.cs
│   ├── PlaySpotApi.csproj
│   ├── dockerfile
│   ├── ...
│
├── .gitignore
├── cleanup.sh
├── dev.sh
├── docker-compose.yml
├── PlaySpot.sln (DEPRECATED)                     # Solution file
├── README.md```
