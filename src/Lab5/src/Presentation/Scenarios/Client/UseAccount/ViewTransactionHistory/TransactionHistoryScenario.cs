using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Spectre.Console;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.ViewTransactionHistory;

public class TransactionHistoryScenario : IScenario
{
    private readonly IAccountService _accountService;

    public TransactionHistoryScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "View transaction history";

    public void Run()
    {
        IEnumerable<TransactionModel> history = _accountService.GetHistory();
        AnsiConsole.MarkupLine("[purple]Operation history:[/]");

        Table table = new Table()
            .AddColumn("[purple]Date & Time[/]")
            .AddColumn("[purple]Type[/]")
            .AddColumn("[purple]Amount[/]");

        foreach (TransactionModel transaction in history)
        {
            table.AddRow(
                transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                transaction.Type.ToString(),
                transaction.Amount.ToString("F2"));
        }

        AnsiConsole.Write(table);
        AnsiConsole.Ask<string>("Ok");
    }
}