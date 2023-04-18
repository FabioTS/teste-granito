using CalculatorApi.Domain.Commands;

namespace CalculatorApi.Domain.Handlers;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> Handle(T command);
}
