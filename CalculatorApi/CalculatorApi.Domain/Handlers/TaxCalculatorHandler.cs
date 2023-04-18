using CalculatorApi.Domain.Commands;
using CalculatorApi.Domain.Services;
using Microsoft.Extensions.Logging;

namespace CalculatorApi.Domain.Handlers;

public class TaxCalculatorHandler :
    IHandler<TaxCalculateCommand>
{
    private readonly ILogger _logger;
    private readonly ITaxService _taxService;
    public TaxCalculatorHandler(ILogger<TaxCalculatorHandler> logger, ITaxService taxService)
    {
        _logger = logger;
        _taxService = taxService;
    }

    public async Task<ICommandResult> Handle(TaxCalculateCommand command)
    {
        try
        {
            // Fail fast validation
            if (!command.Validate())
                return new BadRequestCommandResult(command.Notifications);

            // Get tax value from api
            var tax = await _taxService.GetTax();
            if (!tax.HasValue)
                return new ErrorCommandResult("Falha ao consultar serviço de taxas");

            // Calculate
            var result = command.ValorInicial * Math.Pow(1 + tax.Value, command.Meses);
            // Truncate 2 decimal
            result = Math.Truncate(result * 100) / 100;

            return new SuccessCommandResult(new { result });
        }
        catch (Exception ex)
        {
            var msg = "Falha ao calcular: " + ex.Message;
            _logger.LogError(ex, msg);
            return new ErrorCommandResult(msg);
        }
    }
}
