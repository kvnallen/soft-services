using Juros.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Juros.Controllers
{
    [ApiController]
    public class ShowMeTheCodeController : ControllerBase
    {
        private readonly ILogger<ShowMeTheCodeController> logger;

        public ShowMeTheCodeController(ILogger<ShowMeTheCodeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("showmethecode")]
        [SwaggerOperation("Retorna informações do projeto")]
        [SwaggerResponse(200, type: typeof(ProjectInfo))]
        public IActionResult ShowTheCode([FromServices] IOptions<ProjectInfo> projectInfo)
            => Ok(projectInfo.Value);
    }
}