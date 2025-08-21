namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;

public abstract record LoginResult
{
    public sealed record Success : LoginResult;

    public sealed record Failure : LoginResult;

    public sealed record InvalidPin : LoginResult;
}