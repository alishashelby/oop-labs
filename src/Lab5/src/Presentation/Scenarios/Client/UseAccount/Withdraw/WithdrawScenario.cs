using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Withdraw;

public class WithdrawScenario : IScenario
{
    private readonly IAccountService _accountService;

    public WithdrawScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Withdraw";

    public async void Run()
    {
        decimal withdrawAmount = AnsiConsole.Ask<decimal>("Enter withdrawAmount: ");

        TransactionResult result = await _accountService.Withdraw(withdrawAmount).ConfigureAwait(true);

        if (result is TransactionResult.Failure failure)
        {
            AnsiConsole.MarkupLine($"[red]{failure.Message}[/]");
            AnsiConsole.Ask<string>("Ok");
        }
        else
        {
            AnsiConsole.MarkupLine("[green]Money was withdrawn successfully![/]");
        }
    }
}