# play-spot_backend

Approximate repo structure:
```bash
play-spot_backend/
│
├── .github/
│   └── workflows/
│       └── test-and-deploy.yml       # GitHub Actions workflow
│
├── PlaySpotApi/
│   ├── Data/
│   │   └── PlaySpotDbContext.cs
│   ├── Models/
│   │   └── VenueItem.cs
│   ├── Controllers/
│   │   └── WeatherForecastController.cs
│   ├── Program.cs
│   ├── Startup.cs (if applicable)
│   ├── PlaySpotApi.csproj
│   └── ...
│
├── PlaySpotApi.Tests/
│   ├── SampleVenueTests.cs                # Basic xUnit test
│   └── PlaySpotApi.Tests.csproj
│
├── PlaySpotBackend.sln                     # Solution file
├── render.yaml                       # Optional: Render deployment config
└── README.md```
