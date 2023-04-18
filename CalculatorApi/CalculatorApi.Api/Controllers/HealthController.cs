using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Health Check
    /// </summary>
    [HttpGet]
    public IActionResult HealthCheck()
    {
        _logger.LogInformation(nameof(HealthCheck));
        return Ok("Alive");
    }
}
