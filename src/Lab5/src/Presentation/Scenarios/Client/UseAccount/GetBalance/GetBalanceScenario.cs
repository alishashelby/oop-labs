using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.GetBalance;

public class GetBalanceScenario : IScenario
{
    private readonly IAccountService _accountService;

    public GetBalanceScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Get balance";

    public void Run()
    {
        decimal balance = _accountService.GetBalance();
        AnsiConsole.MarkupLine($"[green]Balance: {balance}[/]");
        AnsiConsole.Ask<string>("Ok");
    }
}