using CalculatorApi.Domain.Commands;

namespace CalculatorApi.Tests.Commands;

[TestClass]
public class TaxCalculateCommandTests
{
    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(50, 10)]
    [DataRow(300, 24)]
    [DataRow(124.55, 6)]
    [DataRow(234.5568, 12)]
    public void ShouldBeValid(double initialValue, int months)
    {
        var command = BuildCommand(initialValue, months);
        Assert.IsTrue(command.Validate());
        Assert.IsTrue(command.IsValid);
    }

    [TestMethod]
    [DataRow(0, 0)]
    [DataRow(1, 0)]
    [DataRow(0, 1)]
    [DataRow(-100, 1)]
    [DataRow(100, -1)]
    [DataRow(-100, -10)]
    [DataRow(-100.3668, 10)]
    public void ShouldBeInvalid(double initialValue, int months)
    {
        var command = BuildCommand(initialValue, months);
        Assert.IsFalse(command.Validate());
        Assert.IsFalse(command.IsValid);
    }

    private TaxCalculateCommand BuildCommand(double initialValue, int months)
    {
        var command = new TaxCalculateCommand();
        command.ValorInicial = initialValue;
        command.Meses = months;
        return command;
    }
}