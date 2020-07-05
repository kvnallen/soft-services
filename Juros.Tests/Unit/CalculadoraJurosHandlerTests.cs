using System;
using System.Threading.Tasks;
using AutoMoqCore;
using FluentAssertions;
using Juros.Clients;
using Juros.Constants;
using Juros.Domain;
using Juros.DTO;
using Moq;
using Xunit;

namespace Juros.Tests.Unit
{
    public class CalculadoraJurosHandlerTests
    {
        private readonly CalculadoraJurosHandler handler;
        private readonly Mock<IJurosClient> jurosClient;

        public CalculadoraJurosHandlerTests()
        {
            var autoMoqer = new AutoMoqer();
            handler = autoMoqer.Create<CalculadoraJurosHandler>();
            jurosClient = autoMoqer.GetMock<IJurosClient>();
        }

        [Fact]
        public void Calcular_QuandoCommandForNull_DeveLancarExcecao()
        {
            handler.Calcular(null).Should();
            handler.Invoking(x => x.Calcular(null))
                .Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData(100, 5, 0.01, 105.10)]
        public async void Calcular_QuandoValoresEstiveremValidos_DeveRetornarValorCalculado(int valorInicial, int meses, double taxaJuros, double valorEsperado)
        {
            //Arrange
            jurosClient.Setup(x => x.GetJuros()).Returns(Task.FromResult(taxaJuros));

            var command = new CalculoCommand
            {
                ValorInicial = valorInicial,
                Meses = meses
            };

            //Act
            var result = await handler.Calcular(command);

            //Assert
            result.Should().Be(valorEsperado);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public void Calcular_QuandoValorForZeroOuNegativo_DeveLancarException(double valorInicial, int meses)
        {
            //Arrange
            var command = new CalculoCommand
            {
                ValorInicial = valorInicial,
                Meses = meses
            };

            //Act // Assert
            handler
                .Invoking(x => x.Calcular(command))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage($"{ValidationErrors.MaiorOuIgualZero}*");
        }
    }
}