using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client;

public class ClientScenarioProvider : IScenarioProvider
{
    private readonly IAccountService _accountService;
    private readonly IEnumerable<IScenario> _accountScenarios;
    private readonly ICurrentAccountService _currentAccountService;

    public ClientScenarioProvider(
        IAccountService accountService,
        IEnumerable<IScenario> accountScenarios,
        ICurrentAccountService currentAccountService)
    {
        _accountService = accountService;
        _accountScenarios = accountScenarios;
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new ClientScenario(_accountService, _currentAccountService, _accountScenarios);
        return true;
    }
}