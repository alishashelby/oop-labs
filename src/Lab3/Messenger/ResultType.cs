namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger;

public abstract record ResultType
{
    public sealed record Success : ResultType;

    public sealed record Failure : ResultType;
}