using CalculatorApi.Domain.Services;

namespace CalculatorApi.Api.Services;

public class TaxService : ITaxService
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;
    private string? _taxPath;
    public TaxService(ILogger<TaxService> logger, IConfiguration config)
    {
        _logger = logger ?? throw new ArgumentNullException("ILogger");
        config = config ?? throw new ArgumentNullException("IConfiguration");
        _taxPath = config.GetValue<string>("GetTaxPath", "tax");

        var url = config.GetValue<string>("TaxApiUrl", string.Empty);
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(url!);

        var errMsg = $"Failed to establish connection to TaxApiUrl. Url = {url}";
        try
        {
            var response = _httpClient.GetAsync("health").Result;
            if (!response.IsSuccessStatusCode)
                throw new Exception(errMsg);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, errMsg);
        }
    }

    public async Task<double?> GetTax()
    {
        _logger.LogDebug($"GET {_taxPath}");

        var response = await _httpClient.GetAsync(_taxPath);

        var result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode || !double.TryParse(result, out var tax))
        {
            _logger.LogError($"POST {_taxPath} Failed. {result}");
            return null;
        }

        return tax;
    }
}
