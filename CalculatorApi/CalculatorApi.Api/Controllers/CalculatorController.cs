using CalculatorApi.Domain.Commands;
using CalculatorApi.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.Api.Controllers;

[ApiController]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;
    private string? _codeUrl;

    public CalculatorController(ILogger<CalculatorController> logger, IConfiguration config)
    {
        _logger = logger;
        _codeUrl = config.GetValue<string?>("CodeUrl");

        if (string.IsNullOrEmpty(_codeUrl))
            _logger.LogWarning("CodeUrl is empty! Set it on appsettings.");
    }

    /// <summary>
    /// Realiza o cálculo de juros compostos
    /// </summary>
    [HttpGet]
    [Route("calculajuros")]
    public async Task<ICommandResult> Calculate(
        [FromQuery] TaxCalculateCommand command,
        [FromServices] TaxCalculatorHandler handler
    )
    {
        _logger.LogInformation(nameof(Calculate));

        var result = await handler.Handle(command);

        Response.StatusCode = (int)result.GetStatus();
        return result;
    }

    /// <summary>
    /// Retorna a URL do repositório git
    /// </summary>
    [HttpGet]
    [Route("showmethecode")]
    public ICommandResult GetCodeUrl()
    {
        _logger.LogInformation(nameof(GetCodeUrl));
        return new SuccessCommandResult(new { url = _codeUrl }, "Obrigado pela oportunidade!");
    }
}
