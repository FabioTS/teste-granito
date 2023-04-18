namespace CalculatorApi.Domain.Services;

public interface ITaxService
{
    Task<double?> GetTax();
}
