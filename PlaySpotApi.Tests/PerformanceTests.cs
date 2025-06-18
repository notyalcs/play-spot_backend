using System.Diagnostics;
using System.Net.Http.Json;
using Xunit;

public class PerformanceTests : IClassFixture<AggregatorWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PerformanceTests(AggregatorWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // testing 100 location requests 
    [Fact]
    public async Task Aggregator_HighLoad_ResponseTimeAcceptable()
    {
        const int requestCount = 100;
        var stopwatch = Stopwatch.StartNew();

        var tasks = Enumerable.Range(0, requestCount).Select(async _ =>
        {
            var response = await _client.GetAsync("/api/Composite/locations");
            response.EnsureSuccessStatusCode();
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        var avgTimePerRequest = stopwatch.Elapsed.TotalMilliseconds / requestCount;
        Assert.True(avgTimePerRequest < 500, $"Average response time too high: {avgTimePerRequest}ms");
    }


    // testing 100 updating fullness request
    [Fact]
    public async Task PostFullness_HighLoad_ResponseTimeAcceptable()
    {
        const int requestCount = 100;
        var stopwatch = Stopwatch.StartNew();

        var tasks = Enumerable.Range(0, requestCount).Select(async i =>
        {
            var payload = new Fullness.Api.Models.Fullness
            {
                LocationId = 1, 
                FullnessLevel = Fullness.Api.Enums.FullnessLevel.Crowded
            };

            var response = await _client.PostAsJsonAsync("/api/Fullness", payload);
            response.EnsureSuccessStatusCode();
        });

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        var avgTimePerRequest = stopwatch.Elapsed.TotalMilliseconds / requestCount;
        Assert.True(avgTimePerRequest < 500, $"Average POST response time too high: {avgTimePerRequest}ms");
    }
}
