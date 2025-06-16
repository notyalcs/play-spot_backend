using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Sport.Api.Data;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SportDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
            tableName: "__EFMigrationsHistory",
            schema: "sport"
        )));

builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",               // for local dev
            "https://play-spot-five.vercel.app",         // for deployed frontend
            "https://play-spot-frontend.vercel.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SportDbContext>();

    // if (app.Environment.IsDevelopment())
    // {
    //     // dbContext.Database.EnsureDeleted();
    //     // dbContext.Database.EnsureCreated();
    // }
    // else
    // {
    //     dbContext.Database.ExecuteSqlRaw(@"
    //         DO $$ DECLARE
    //             r RECORD;
    //         BEGIN
    //             EXECUTE 'DROP SCHEMA sport CASCADE';
    //             EXECUTE 'CREATE SCHEMA IF NOT EXISTS sport';
    //         END $$;");
    // }

    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }

    SeedData.SeedDatabase(dbContext);
}

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

public partial class Program { }
