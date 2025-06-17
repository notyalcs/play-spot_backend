using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Sport.Api.Data;

namespace PlaySpotApi.Tests
{
    public class SportWebApplicationFactory : WebApplicationFactory<SportProgram>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<SportDbContext>))
                    .ToList()
                    .ForEach(d =>
                    {
                        services.Remove(d);
                    });

                services.AddDbContext<SportDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                var dbSport = scope.ServiceProvider.GetRequiredService<SportDbContext>();

                dbSport.Database.EnsureCreated();

                dbSport.SaveChanges();
            });
        }
    }
}
