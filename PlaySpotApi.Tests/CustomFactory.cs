using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using PlaySpotApi.Data;

namespace PlaySpotApi.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<PlaySpotDbContext>))
                    .ToList()
                    .ForEach(d =>
                    {
                        services.Remove(d);
                    });

                services.AddDbContext<PlaySpotDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PlaySpotDbContext>();
                db.Database.EnsureCreated();

                db.SaveChanges();
            });
        }
    }
}
