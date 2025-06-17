using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

// using Admin.Api.Data;

namespace PlaySpotApi.Tests
{
    public class AdminWebApplicationFactory : WebApplicationFactory<AdminProgram>
    {
        // private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // services.Where(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<AdminDbContext>))
                //     .ToList()
                //     .ForEach(d =>
                //     {
                //         services.Remove(d);
                //     });

                // services.AddDbContext<AdminDbContext>(options =>
                // {
                //     options.UseInMemoryDatabase(_dbName);
                // });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                // var dbAdmin = scope.ServiceProvider.GetRequiredService<AdminDbContext>();

                // dbAdmin.Database.EnsureCreated();

                // dbAdmin.SaveChanges();
            });
        }
    }
}
