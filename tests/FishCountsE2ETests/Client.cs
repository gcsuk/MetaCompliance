using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FishCountsE2ETests;

internal static class Client
{
    public static HttpClient Instance { get; }

    static Client()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "EcologyApiBaseUrl", "https://environment.data.gov.uk/ecology/api/v1/" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings.ToList())
            .Build();

        // Create the WebHostBuilder with the in-memory configuration
        var webHost = new WebHostBuilder()
            .UseEnvironment(Environments.Development)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddConfiguration(configuration);
            })
            .UseStartup<Startup>();

        var testServer = new TestServer(webHost);
        Instance = testServer.CreateClient();
    }
}
