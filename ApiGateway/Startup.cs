using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;

namespace ApiGateway
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration) => this.configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddLogging()
                .AddSwaggerForOcelot(configuration)
                .AddOcelot(configuration)
                .AddEureka();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
            => app.UseSwaggerForOcelotUI(opt => opt.PathToSwaggerGenerator = "/swagger/docs")
                .UseOcelot()
                .Wait();
    }
}
