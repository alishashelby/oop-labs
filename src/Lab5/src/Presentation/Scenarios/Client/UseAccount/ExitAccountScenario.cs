using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount;

public class ExitAccountScenario : IScenario
{
    private readonly ICurrentAccountService _currentAccountService;

    public ExitAccountScenario(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    public string Name => "Exit";

    public void Run()
    {
        _currentAccountService.Account = null;
    }
}