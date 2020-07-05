using System.Net.Http;
using FluentAssertions;
using Juros.Options;
using Juros.Tests.Config;
using Steeltoe.Common.Http;
using Xunit;

namespace Juros.Tests.Integration
{
    public class ShowMeTheCodeControllerIntegrationTests
    {
        private readonly HttpClient client;

        public ShowMeTheCodeControllerIntegrationTests()
        {
            var factory = new CustomWebAppFactory<Startup>();
            client = factory.CreateClient();
        }

        [Fact]
        public async void ShowTheCode_QuandoConfiguracaoExistir_DeveRetornarInformacoesDoProjeto()
        {
            //Act
            var result = await client.GetAsync("/showmethecode");
            var content = await result.Content.ReadAsJsonAsync<ProjectInfo>();

            //Assert
            result.EnsureSuccessStatusCode();
            content.Name.Should().Be("Softplan Services");
            content.RepositoryUrl.Should().Be("https://github.com/kvnallen/softplan-services");
        }
    }
}