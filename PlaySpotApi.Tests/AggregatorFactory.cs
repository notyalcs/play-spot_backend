using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace PlaySpotApi.Tests
{
    // Factory for Aggregator service, also starts Location, Sport, and Fullness services for integration tests
    public class AggregatorWebApplicationFactory : WebApplicationFactory<AggregatorProgram>
    {
        private LocationWebApplicationFactory? _locationFactory;
        private SportWebApplicationFactory? _sportFactory;
        private FullnessWebApplicationFactory? _fullnessFactory;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _locationFactory = new LocationWebApplicationFactory();
            _sportFactory = new SportWebApplicationFactory();
            _fullnessFactory = new FullnessWebApplicationFactory();

            var locationClient = _locationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://location/")
            });
            var sportClient = _sportFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://sport/")
            });
            var fullnessClient = _fullnessFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://fullness/")
            });

            builder.ConfigureServices(services =>
            {
                services.Where(d => d.ServiceType == typeof(IHttpClientFactory))
                    .ToList()
                    .ForEach(d =>
                    {
                        services.Remove(d);
                    });

                services.AddHttpClient("LocationService", c =>
                {
                    c.BaseAddress = locationClient.BaseAddress;
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    _locationFactory.Server.CreateHandler()!);

                services.AddHttpClient("SportService", c =>
                {
                    c.BaseAddress = sportClient.BaseAddress;
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    _sportFactory.Server.CreateHandler()!);

                services.AddHttpClient("FullnessService", c =>
                {
                    c.BaseAddress = fullnessClient.BaseAddress;
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    _fullnessFactory.Server.CreateHandler()!);
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _locationFactory?.Dispose();
            _sportFactory?.Dispose();
            _fullnessFactory?.Dispose();
        }
    }
}
