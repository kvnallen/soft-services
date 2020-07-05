using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CalculadoraJuros;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Http;
using Xunit;

namespace Taxa.Tests.Integration
{
    public class CalculadoraControllerIntegrationTests
    {
        public CalculadoraControllerIntegrationTests()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(config =>
                {
                    config.UseTestServer();
                    config.UseEnvironment("Test");
                    config.UseConfiguration(new ConfigurationBuilder()
                        .SetBasePath(currentPath)
                        .AddJsonFile("appsettings.json")
                        .Build());

                    config.UseStartup<Startup>();
                });

            var host = hostBuilder.Start();
            client = host.GetTestClient();
        }

        private readonly HttpClient client;


        [Fact]
        public async Task GetTaxa_DeveRetornarTaxa()
        {
            //Act
            var response = await client.GetAsync("/taxaJuros");
            var taxa = await response.Content.ReadAsJsonAsync<double>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            taxa.Should().Be(0.01d);
        }
    }
}