using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// using Aggregator.Api.Data;

namespace PlaySpotApi.Tests
{
    public class AggregatorWebApplicationFactory : WebApplicationFactory<AggregatorProgram>
    {
        // private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<AggregatorDbContext>))
                //     .ToList()
                //     .ForEach(d =>
                //     {
                //         services.Remove(d);
                //     });

                // services.AddDbContext<AggregatorDbContext>(options =>
                // {
                //     options.UseInMemoryDatabase(_dbName);
                // });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                // var dbAggregator = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

                // dbAggregator.Database.EnsureCreated();

                // dbAggregator.SaveChanges();
            });
        }
    }
}
