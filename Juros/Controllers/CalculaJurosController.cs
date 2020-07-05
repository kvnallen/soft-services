using System.Threading.Tasks;
using Juros.Domain;
using Juros.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Juros.Controllers
{
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        [HttpGet("calculajuros")]
        public async Task<IActionResult> Calcular(
            [FromServices] CalculadoraJurosHandler handler,
            [FromQuery] CalculoCommand command)
        {
            var result = await handler.Calcular(command);
            return Ok(result);
        }
    }
}