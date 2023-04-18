using System.Net;
using System.Text.Json;
using CalculatorApi.Domain.Commands;
using CalculatorApi.Domain.Handlers;
using CalculatorApi.Tests.Services;
using Microsoft.Extensions.Logging;

namespace CalculatorApi.Tests.Handlers;

[TestClass]
public class TaxCalculatorHandlerTests
{
    private TaxCalculatorHandler _handler;
    public TaxCalculatorHandlerTests()
    {
        _handler = BuildHandler();
    }

    [TestMethod]
    [DataRow(1, 1, 1)]
    [DataRow(50, 10, 1)]
    [DataRow(300, 24, 1)]
    [DataRow(124.55, 6, 1)]
    [DataRow(234.5568, 12, 1)]
    public void ShouldBeOkAndCorrect(double initialValue, int months, double expected)
    {
        var command = BuildCommand(initialValue, months);
        var result = _handler.Handle(command).Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.GetStatus());

        var json = JsonSerializer.SerializeToDocument(result.GetData());
        var calculated = json.RootElement.GetProperty("data").GetProperty("result").GetDouble();
        Assert.AreEqual(expected, calculated);
    }

    private TaxCalculateCommand BuildCommand(double initialValue, int months)
    {
        var command = new TaxCalculateCommand();
        command.ValorInicial = initialValue;
        command.Meses = months;
        return command;
    }

    private TaxCalculatorHandler BuildHandler()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
        );

        var logger = loggerFactory.CreateLogger<TaxCalculatorHandler>();
        var fakeTaxService = new FakeTaxService();

        var handler = new TaxCalculatorHandler(logger, fakeTaxService);
        return handler;
    }
}