using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Fullness.Api.Data;

namespace PlaySpotApi.Tests
{
    public class FullnessWebApplicationFactory : WebApplicationFactory<FullnessProgram>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<FullnessDbContext>))
                    .ToList()
                    .ForEach(d =>
                    {
                        services.Remove(d);
                    });

                services.AddDbContext<FullnessDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                var dbFullness = scope.ServiceProvider.GetRequiredService<FullnessDbContext>();

                dbFullness.Database.EnsureCreated();

                dbFullness.SaveChanges();
            });
        }
    }
}
