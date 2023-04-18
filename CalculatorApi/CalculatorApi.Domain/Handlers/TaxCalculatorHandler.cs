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
            if (!command.Validate())
                return new BadRequestCommandResult(command.Notifications);

            var tax = await _taxService.GetTax();
            if (tax == null)
                return new ErrorCommandResult("Falha ao consultar serviço de taxas");

            var result = Math.Truncate(command.ValorInicial * ((1 + tax) ^ command.Meses), 2);

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
