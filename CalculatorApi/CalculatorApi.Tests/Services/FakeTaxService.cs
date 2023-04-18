using CalculatorApi.Domain.Services;

namespace CalculatorApi.Tests.Services;

public class FakeTaxService : ITaxService
{
    public async Task<double?> GetTax()
    {
        return await Task.FromResult(0.01);
    }
}