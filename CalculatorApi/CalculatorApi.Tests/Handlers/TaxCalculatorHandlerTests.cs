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
    [DataRow(1, 1, 1.01)]
    [DataRow(100, 5, 105.10)]
    [DataRow(300, 24, 380.92)]
    [DataRow(124.55, 6, 132.21)]
    [DataRow(234.5568, 12, 264.30)]
    public void ShouldBeOkAndCorrect(double initialValue, int months, double expected)
    {
        var command = BuildCommand(initialValue, months);
        var result = _handler.Handle(command).Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.GetStatus());

        var json = JsonSerializer.SerializeToDocument(result.GetData());
        var calculated = json.RootElement.GetProperty("result").GetDouble();
        Assert.AreEqual(expected, calculated);
    }

    [TestMethod]
    [DataRow(1, 1, 1.011)]
    [DataRow(100, 5, 105.102)]
    [DataRow(50.5568, 24, 264.30)]
    public void ShouldBeOkButIncorrect(double initialValue, int months, double expected)
    {
        var command = BuildCommand(initialValue, months);
        var result = _handler.Handle(command).Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.GetStatus());

        var json = JsonSerializer.SerializeToDocument(result.GetData());
        var calculated = json.RootElement.GetProperty("result").GetDouble();
        Assert.AreNotEqual(expected, calculated);
    }

    [TestMethod]
    [DataRow(0, 0)]
    [DataRow(1, 0)]
    [DataRow(0, 1)]
    [DataRow(-100, 1)]
    [DataRow(100, -1)]
    [DataRow(-100, -10)]
    [DataRow(-100.3668, 10)]
    public void ShouldBeBadRequest(double initialValue, int months)
    {
        var command = BuildCommand(initialValue, months);
        var result = _handler.Handle(command).Result;
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.GetStatus());
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