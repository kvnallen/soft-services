using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Juros.Clients;
using Juros.Constants;
using Juros.Tests.Config;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Steeltoe.Common.Http;
using Xunit;

namespace Juros.Tests.Integration
{
    public class CalculaJurosControllerIntegrationTests
    {
        private readonly HttpClient client;
        private readonly Mock<IJurosClient> jurosClientMock;

        public CalculaJurosControllerIntegrationTests()
        {
            jurosClientMock = new Mock<IJurosClient>();
            var factory = new CustomWebAppFactory<Startup>(ChangeDependency);
            client = factory.CreateClient();
        }

        private void ChangeDependency(IServiceCollection services)
        {
            var currentService = services.Where(x => x.ServiceType == typeof(IJurosClient)).ToList();
            currentService.ForEach(sd => services.Remove(sd));
            services.AddSingleton(jurosClientMock.Object);
        }

        [Theory]
        [InlineData(0.1, 5, 0.01d, "1.05")]
        [InlineData(100, 5, 0.01d, "105.10")]
        public async void Calcular_DeveRetornarValorCalculado(double valorInicial, int meses, double taxaJuros, string valorEsperado)
        {
            //Arrange
            jurosClientMock.Setup(x => x.GetJuros()).Returns(Task.FromResult(taxaJuros));

            //Act
            var result = await client.GetAsync($"/calculajuros?valorinicial={valorInicial}&meses={meses}");
            var valor = await result.Content.ReadAsJsonAsync<string>();

            //Assert
            result.EnsureSuccessStatusCode();
            valor.Should().Be(valorEsperado);
        }


        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 0)]
        public async void Calcular_QuandoMesOuValorForZero_DeveRetornarListaErros(double valorInicial, int meses)
        {
            //Act
            var result = await client.GetAsync($"/calculajuros?valorinicial={valorInicial}&meses={meses}");
            var content = await result.Content.ReadAsStringAsync();

            //Assert
            content.Should().Contain(ValidationErrors.MaiorOuIgualZero);
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
