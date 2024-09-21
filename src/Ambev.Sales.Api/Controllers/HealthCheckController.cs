using Microsoft.AspNetCore.Mvc;

namespace Ambev.Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("ping")]
        [ProducesResponseType(200)]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
