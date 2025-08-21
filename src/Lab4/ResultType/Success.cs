namespace Itmo.ObjectOrientedProgramming.Lab4.ResultType;

public class Success<T> : IResultType<T>
{
    public T? Result { get; }

    public string Message { get; }

    public Success(string message, T? result = default)
    {
        Result = result;
        Message = message;
    }
}