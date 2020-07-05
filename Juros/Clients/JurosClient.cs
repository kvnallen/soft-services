using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Polly.Retry;
using RestEase;
using Steeltoe.Common.Discovery;

namespace Juros.Clients
{
    public class JurosClient : IJurosClient
    {
        private readonly IJurosClient client;
        private static readonly AsyncRetryPolicy RetryPolicy = Policy
            .HandleInner<Exception>()
            .WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(3));

        public JurosClient(IDiscoveryClient discoveryClient, IConfiguration configuration)
        {
            var handler = new DiscoveryHttpClientHandler(discoveryClient);
            var serviceUri = configuration.GetValue<string>("Api1Uri");
            var httpClient = new HttpClient(handler, false) { BaseAddress = new Uri(serviceUri) };
            client = RestClient.For<IJurosClient>(httpClient);
        }

        public Task<double> GetJuros()
            => RetryPolicy.ExecuteAsync(async () => await client.GetJuros());
    }
}
