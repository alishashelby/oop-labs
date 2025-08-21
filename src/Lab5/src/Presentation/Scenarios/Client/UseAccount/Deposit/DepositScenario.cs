using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Deposit;

public class DepositScenario : IScenario
{
    private readonly IAccountService _accountService;

    public DepositScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Deposit";

    public async void Run()
    {
        AnsiConsole.Clear();
        decimal depositAmount = AnsiConsole.Ask<decimal>("Enter deposit amount: ");

        TransactionResult result = await _accountService.Deposit(depositAmount).ConfigureAwait(true);

        if (result is TransactionResult.Failure failure)
        {
            AnsiConsole.MarkupLine($"[red]{failure.Message}[/]");
            AnsiConsole.Ask<string>("Ok");
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Money was deposited successfully![/]");
        }
    }
}