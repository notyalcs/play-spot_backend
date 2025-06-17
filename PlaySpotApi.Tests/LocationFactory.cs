using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Location.Api.Data;

namespace PlaySpotApi.Tests
{
    public class LocationWebApplicationFactory : WebApplicationFactory<LocationProgram>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<LocationDbContext>))
                    .ToList()
                    .ForEach(d =>
                    {
                        services.Remove(d);
                    });

                services.AddDbContext<LocationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                var dbLocation = scope.ServiceProvider.GetRequiredService<LocationDbContext>();

                dbLocation.Database.EnsureCreated();

                dbLocation.SaveChanges();
            });
        }
    }
}
