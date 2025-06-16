using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Fullness.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FullnessDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
            tableName: "__EFMigrationsHistory",
            schema: "fullness"
        )));

builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",               // for local dev
            "https://play-spot-five.vercel.app"         // for deployed frontend
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FullnessDbContext>();

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
    //             EXECUTE 'DROP SCHEMA fullness CASCADE';
    //             EXECUTE 'CREATE SCHEMA IF NOT EXISTS fullness';
    //         END $$;");
    // }

    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
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
