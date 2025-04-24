using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PlaySpotApi.Data;
using PlaySpotApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlaySpotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PlaySpotDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to PlaySpot API! This is a test.")
    .WithName("GetRoot")
    .WithOpenApi()
    .Produces<string>(StatusCodes.Status200OK);

app.MapGet("/venues", async (PlaySpotDbContext db) =>
{
    var venues = await db.VenueItems.ToListAsync();
    return venues;
})
.WithName("GetVenues")
    .WithOpenApi()
    .Produces<List<VenueItem>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status500InternalServerError);

app.MapPost("/venues", async (PlaySpotDbContext db, VenueItem venue) =>
{
    db.VenueItems.Add(venue);
    await db.SaveChangesAsync();
    return Results.Created($"/venues/{venue.Id}", venue);
})
.WithName("CreateVenue")
    .WithOpenApi()
    .Produces<VenueItem>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError);

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
