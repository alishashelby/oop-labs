using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.CreateAccount;

public class CreateAccountScenario : IScenario
{
    private readonly IAccountService _accountService;

    public CreateAccountScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Create new account";

    public async void Run()
    {
        int accountId = AnsiConsole.Ask<int>("Enter accountId: ");
        string pin = AnsiConsole.Ask<string>("Create pin: ");

        LoginResult result = await _accountService.RegisterAccount(accountId, pin).ConfigureAwait(false);
        switch (result)
        {
            case LoginResult.InvalidPin:
                AnsiConsole.MarkupLine("[red]Please enter a pin to use.");
                AnsiConsole.Ask<string>("Ok");
                return;
            case LoginResult.Failure:
                AnsiConsole.MarkupLine("[red]Failed to create account.[/]");
                break;
            case LoginResult.Success:
                AnsiConsole.MarkupLine("[green]Account successfully registered![/]");
                break;
        }
    }
}