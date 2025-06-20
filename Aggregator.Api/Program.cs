using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",               // for local dev
            "http://localhost:5432",
            "http://localhost:5020",
            "http://localhost:5188",
            "http://localhost:5048",
            "http://localhost:5043",
            "https://play-spot-five.vercel.app",         // for deployed frontend
            "https://play-spot-frontend.vercel.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddHttpClient("LocationService", client =>
{
    var locationServiceUrl = builder.Configuration.GetValue<string>("Services:Location");
    if (string.IsNullOrEmpty(locationServiceUrl))
    {
        throw new InvalidOperationException("Configuration value for 'Services:Location' is missing.");
    }
    client.BaseAddress = new Uri(locationServiceUrl);
});
builder.Services.AddHttpClient("SportService", client =>
{
    var sportServiceUrl = builder.Configuration.GetValue<string>("Services:Sport");
    if (string.IsNullOrEmpty(sportServiceUrl))
    {
        throw new InvalidOperationException("Configuration value for 'Services:Sport' is missing.");
    }
    client.BaseAddress = new Uri(sportServiceUrl);
});
builder.Services.AddHttpClient("FullnessService", client =>
{
    var fullnessServiceUrl = builder.Configuration.GetValue<string>("Services:Fullness");
    if (string.IsNullOrEmpty(fullnessServiceUrl))
    {
        throw new InvalidOperationException("Configuration value for 'Services:Fullness' is missing.");
    }
    client.BaseAddress = new Uri(fullnessServiceUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class AggregatorProgram { }
