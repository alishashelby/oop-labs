using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Login;

public class ClientLoginScenario : IScenario
{
    private readonly IAccountService _accountService;

    public ClientLoginScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Login";

    public void Run()
    {
        while (true)
        {
            string[] scenarios =
            [
                "Enter login details",
                "Cancel",
            ];

            SelectionPrompt<string> selector = new SelectionPrompt<string>()
                .Title("Select")
                .AddChoices(scenarios);

            string scenario = AnsiConsole.Prompt(selector);

            if (scenario == "Cancel")
                return;

            int accountId = AnsiConsole.Ask<int>("Enter accountId: ");
            string pin = AnsiConsole.Ask<string>("Enter pin: ");
            LoginResult result = _accountService.Login(accountId, pin);

            AnsiConsole.Clear();
            if (result is LoginResult.Failure)
            {
                AnsiConsole.MarkupLine("[red]Login failed. Try again.[/]");
                return;
            }

            AnsiConsole.MarkupLine("[green]Login successful![/]");
            return;
        }
    }
}