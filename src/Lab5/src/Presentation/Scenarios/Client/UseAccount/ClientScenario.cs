using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.CreateAccount;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Login;
using Spectre.Console;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount;

public class ClientScenario : IScenario
{
    private readonly IAccountService _accountService;
    private readonly ICurrentAccountService _currentAccountService;

    private readonly IEnumerable<IScenario> _accountScenarios;

    public ClientScenario(
        IAccountService accountService,
        ICurrentAccountService currentAccountService,
        IEnumerable<IScenario> accountScenarios)
    {
        _accountService = accountService;
        _accountScenarios = accountScenarios;
        _currentAccountService = currentAccountService;
    }

    public string Name => "Client";

    public void Run()
    {
        while (true)
        {
            if (_currentAccountService.Account is not null)
            {
                var accountMenu = new AccountScenario(_accountScenarios, _currentAccountService);
                accountMenu.Run();
                AnsiConsole.Clear();

                return;
            }

            IEnumerable<IScenario> scenarios = GetScenarios();
            SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
                .Title("Select")
                .AddChoices(scenarios)
                .UseConverter(x => x.Name);

            IScenario selectedScenario = AnsiConsole.Prompt(selector);

            if (selectedScenario is BackScenario)
            {
                return;
            }

            selectedScenario.Run();
        }
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        yield return new ClientLoginScenario(_accountService);
        yield return new CreateAccountScenario(_accountService);
        yield return new BackScenario();
    }
}