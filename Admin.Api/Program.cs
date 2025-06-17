using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<AdminDbContext>(options =>
//     options.UseNpgsql(
//         builder.Configuration.GetConnectionString("DefaultConnection"),
//         npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
//             tableName: "__EFMigrationsHistory",
//             schema: "admin"
//         )));

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

public partial class AdminProgram { }
