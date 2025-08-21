using System;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;

public class TransactionModel
{
    public Guid Id { get; }

    public int AccountId { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public TransactionType Type { get; }

    public TransactionModel(int accountId, DateTime date, decimal amount, TransactionType type)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Date = date;
        Amount = amount;
        Type = type;
    }
}