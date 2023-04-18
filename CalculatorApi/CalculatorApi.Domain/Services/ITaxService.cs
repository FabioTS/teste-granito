namespace CalculatorApi.Domain.Services;

public interface ITaxService
{
    Task<decimal?> GetTax();
}
