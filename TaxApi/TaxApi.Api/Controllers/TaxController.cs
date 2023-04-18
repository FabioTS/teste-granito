using Microsoft.AspNetCore.Mvc;

namespace TaxApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaxController : ControllerBase
{
    private readonly ILogger<TaxController> _logger;
    private decimal _taxValue;

    public TaxController(ILogger<TaxController> logger, IConfiguration config)
    {
        _logger = logger;
        _taxValue = config.GetValue<decimal>("TaxValue", (decimal)0.01);
    }

    /// <summary>
    /// Consulta a taxa de juros
    /// </summary>
    [HttpGet]
    public IActionResult TaxGet()
    {
        _logger.LogInformation(nameof(TaxGet));
        return Ok(_taxValue);
    }
}
