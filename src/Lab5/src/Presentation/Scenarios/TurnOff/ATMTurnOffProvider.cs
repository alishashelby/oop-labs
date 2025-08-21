using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.TurnOff;

public class ATMTurnOffProvider : IScenarioProvider
{
    private readonly ICurrentAccountService _currentAccountService;

    public ATMTurnOffProvider(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new ATMTurnOff();
        return true;
    }
}