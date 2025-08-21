namespace Itmo.ObjectOrientedProgramming.Lab4.ResultType;

public class Failure<T> : IResultType<T>
{
    public T? Result { get; set; }

    public string Message { get; set; }

    public Failure(string message)
    {
        Message = message;
    }
}