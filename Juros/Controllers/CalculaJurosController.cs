using System.Threading.Tasks;
using Juros.Domain;
using Juros.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Juros.Controllers
{
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        [HttpGet("calculajuros")]
        [SwaggerOperation("Faz um cálculo em memória, de juros compostos, conforme abaixo: Valor Final = Valor Inicial * (1 + juros) ^ Tempo")]
        [SwaggerResponse(200, "Valor calculado com sucesso.", typeof(double))]
        [SwaggerResponse(400, "Parâmetro faltando")]
        public async Task<IActionResult> Calcular(
            [FromServices] CalculadoraJurosHandler handler,
            [SwaggerParameter(Required = true)][FromQuery] CalculoCommand command)
        {
            var result = await handler.Calcular(command);
            return Ok(result);
        }
    }
}