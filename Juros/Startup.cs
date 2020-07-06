using System.Reflection;
using FluentValidation.AspNetCore;
using Juros.Clients;
using Juros.Domain;
using Juros.Formatters;
using Juros.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace Juros
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "API2", Version = "v1"});
                c.EnableAnnotations();
            });
            services.AddDiscoveryClient(Configuration);
            services.AddControllers(cfg => cfg.OutputFormatters.Insert(0, new DoubleTwoDecimalPlacesFormatter()))
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.Configure<ProjectInfo>(Configuration.GetSection("ProjectInfo"));
            services.AddScoped<CalculadoraJurosHandler>();
            services.AddSingleton<IJurosClient, JurosClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseDiscoveryClient();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
