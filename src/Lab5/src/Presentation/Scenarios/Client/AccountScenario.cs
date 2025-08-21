using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Spectre.Console;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client;

public class AccountScenario : IScenario
{
    private readonly IEnumerable<IScenario> _scenarios;
    private readonly ICurrentAccountService _currentAccountService;

    public AccountScenario(IEnumerable<IScenario> scenarios, ICurrentAccountService currentAccountService)
    {
        _scenarios = scenarios;
        _currentAccountService = currentAccountService;
    }

    public string Name => "Account";

    public void Run()
    {
        while (true)
        {
            if (_currentAccountService.Account is null)
            {
                return;
            }

            SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
                .Title("Select")
                .AddChoices(_scenarios)
                .UseConverter(x => x.Name);

            IScenario scenario = AnsiConsole.Prompt(selector);
            scenario.Run();
            AnsiConsole.Clear();
        }
    }
}