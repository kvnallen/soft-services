using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Juros.Tests.Config
{
    internal class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public CustomWebAppFactory() : this(null)
        {
        }

        public CustomWebAppFactory(Action<IServiceCollection> registrations = null)
        {
            Registrations = registrations ?? (collection => { });
        }

        public Action<IServiceCollection> Registrations { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build());

            builder.UseEnvironment("Test");
            builder.ConfigureTestServices(services => { Registrations?.Invoke(services); });
        }
    }
}