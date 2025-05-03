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
├── PlaySpotApi/
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
├── PlaySpotApi.Tests/
│   ├── SampleVenueTests.cs                # Basic xUnit test
│   ├── PlaySpotApi.Tests.csproj
│
├── .gitignore
├── cleanup.sh
├── dev.sh
├── PlaySpotBackend.sln                     # Solution file
├── README.md```
