using System.Net;

namespace CalculatorApi.Domain.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult(HttpStatusCode httpStatusCode,
                         bool success,
                         string? message = null,
                         object? data = null)
    {
        HttpStatusCode = httpStatusCode;
        Success = success;
        Message = message ?? httpStatusCode.ToString();
        Data = data;
    }

    public HttpStatusCode HttpStatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }

    public HttpStatusCode GetStatus() => this.HttpStatusCode;
    public bool Succeeded() => this.Success;
    public string GetMessage() => this.Message;
    public object? GetData() => this.Data;
    public CommandResult GetResult() => this;
}
public class SuccessCommandResult : CommandResult
{
    public SuccessCommandResult(
        object? data = null,
        string? message = null,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        : base(httpStatusCode, true, message, data)
    { }
}
public class BadRequestCommandResult : CommandResult
{
    public BadRequestCommandResult(
        object? data = null,
        string? message = null)
        : base(HttpStatusCode.BadRequest, false, message, data)
    { }
}
public class NotFoundCommandResult : CommandResult
{
    public NotFoundCommandResult(
        object? data = null,
        string? message = null)
        : base(HttpStatusCode.NotFound, false, message, data)
    { }
}
public class UnauthorizedCommandResult : CommandResult
{
    public UnauthorizedCommandResult(
        string? message = null,
        object? data = null)
        : base(HttpStatusCode.Unauthorized, false, message, data)
    { }
}
public class ErrorCommandResult : CommandResult
{
    public ErrorCommandResult(
        string? message = null,
        object? data = null)
        : base(HttpStatusCode.InternalServerError, false, message, data)
    { }
}
