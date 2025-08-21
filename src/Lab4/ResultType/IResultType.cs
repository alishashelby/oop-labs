namespace Itmo.ObjectOrientedProgramming.Lab4.ResultType;

public interface IResultType<T>
{
    T? Result { get; }

    string Message { get; }
}