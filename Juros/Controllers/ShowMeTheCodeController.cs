using Juros.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        public IActionResult ShowTheCode([FromServices] IOptions<ProjectInfo> projectInfo) 
            => Ok(projectInfo.Value);
    }
}