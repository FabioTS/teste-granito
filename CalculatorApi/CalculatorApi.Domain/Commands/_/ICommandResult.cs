using System.Net;

namespace CalculatorApi.Domain.Commands;

public interface ICommandResult
{
    HttpStatusCode GetStatus();
    bool Succeeded();
    string GetMessage();
    object? GetData();
    CommandResult GetResult();
}
