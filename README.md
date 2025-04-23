# play-spot_backend

Approximate repo structure:
```bash
MyApiRepo/
│
├── .github/
│   └── workflows/
│       └── test-and-deploy.yml       # GitHub Actions workflow
│
├── MyApi/
│   ├── Controllers/
│   │   └── WeatherForecastController.cs
│   ├── Program.cs
│   ├── Startup.cs (if applicable)
│   ├── MyApi.csproj
│   └── ...
│
├── MyApi.Tests/
│   ├── SampleTests.cs                # Basic xUnit test
│   └── MyApi.Tests.csproj
│
├── MyApiRepo.sln                     # Solution file
├── render.yaml                       # Optional: Render deployment config
└── README.md```
