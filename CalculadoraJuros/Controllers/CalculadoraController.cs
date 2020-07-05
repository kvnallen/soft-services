using Microsoft.AspNetCore.Mvc;

namespace Taxa.Controllers
{
    [ApiController]
    public class CalculadoraController : ControllerBase
    {
        [HttpGet("taxaJuros")]
        public double GetTaxaJuros() => 0.01d;
    }
}
