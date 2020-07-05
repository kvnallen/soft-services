using System;
using System.Threading.Tasks;
using Juros.Clients;
using Juros.Constants;
using Juros.DTO;
using Microsoft.Extensions.Logging;

namespace Juros.Domain
{
    public class CalculadoraJurosHandler
    {
        private readonly IJurosClient jurosClient;
        private readonly ILogger<CalculadoraJurosHandler> logger;

        public CalculadoraJurosHandler(IJurosClient jurosClient,
             ILogger<CalculadoraJurosHandler> logger)
        {
            this.jurosClient = jurosClient;
            this.logger = logger;
        }

        public async Task<double> Calcular(CalculoCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (command.ValorInicial <= 0) throw new ArgumentOutOfRangeException("valorInicial", ValidationErrors.MaiorOuIgualZero);
            if (command.Meses <= 0) throw new ArgumentOutOfRangeException("meses", ValidationErrors.MaiorOuIgualZero);

            logger.LogInformation("Obtendo júros da API 1");
            var juros = await jurosClient.GetJuros();
            logger.LogInformation("Júros carregado com uma taxa de {0}", juros);

            var valor = command.ValorInicial * Math.Pow(1 + juros, command.Meses);
            return (double)decimal.Round((decimal)valor, 2);
        }
    }
}