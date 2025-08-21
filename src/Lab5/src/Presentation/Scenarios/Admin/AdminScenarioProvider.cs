using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Admin;
using System.Diagnostics.CodeAnalysis;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Admin;

public class AdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly ICurrentAccountService _currentAccountService;

    public AdminScenarioProvider(IAdminService adminService, ICurrentAccountService currentAccountService)
    {
        _adminService = adminService;
        _currentAccountService = currentAccountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAccountService.Account is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new AdminScenario(_adminService);
        return true;
    }
}