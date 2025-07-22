using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Gets the health status of the application.
        /// </summary>
        /// <returns>A JSON object with status and UTC time.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "Healthy",
                time = DateTime.UtcNow
            });
        }



        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var identity = HttpContext.User.Identity;

            return Ok(new
            {
                isAuthenticated = identity?.IsAuthenticated,
                name = identity?.Name,
                claims = HttpContext.User.Claims.Select(x => new { x.Type, x.Value })
            });
        }
    }
}
