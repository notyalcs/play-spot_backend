using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PlaySpotApi.Data;
using PlaySpotApi.Routes;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlaySpotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PlaySpotDbContext>();
    
    if (app.Environment.IsDevelopment())
    {
        dbContext.Database.EnsureDeleted(); // Ensure the database is deleted. Comment this line if you want to keep the database.
    }
    else
    {
        // Comment out the raw SQL execution if you want to keep the database schema and data.
        // dbContext.Database.ExecuteSqlRaw(@"
        //     DO $$ DECLARE
        //         r RECORD;
        //     BEGIN
        //         EXECUTE 'DROP SCHEMA public CASCADE';
        //         EXECUTE 'CREATE SCHEMA public';
        //     END $$;");
    }

    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }

    SeedData.SeedDatabase(dbContext); // Seed the database with initial data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // This is at http://localhost:5102/scalar/v1 when running locally
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok("Healthy"))
    .WithName("GetHealth")
    .WithOpenApi()
    .Produces<string>(StatusCodes.Status200OK);

var locationGroup = app.MapGroup("/locations");
locationGroup.MapLocationRoutes();

app.MapLocationActivityRoutes();

var sportGroup = app.MapGroup("/sports");
sportGroup.MapSportRoutes();

app.Run();

public partial class Program { }
