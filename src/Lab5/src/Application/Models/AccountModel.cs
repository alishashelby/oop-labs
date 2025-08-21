namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;

public class AccountModel
{
    public AccountModel(int id, string pin, decimal balance)
    {
        Id = id;
        Pin = pin;
        Balance = balance;
    }

    public int Id { get; }

    public string? Pin { get; }

    public decimal Balance { get; set; }
}