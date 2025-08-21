using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public interface ICommand
{
    IResultType<ICommand> Execute();
}