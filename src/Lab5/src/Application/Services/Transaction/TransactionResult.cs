namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;

public abstract record TransactionResult
{
    public sealed record Success : TransactionResult;

    public sealed record Failure(string Message) : TransactionResult;
}