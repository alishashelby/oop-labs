using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios;
using Spectre.Console;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation;

public class ATMRunner
{
    private readonly IEnumerable<IScenarioProvider> _providers;

    public ATMRunner(IEnumerable<IScenarioProvider> providers)
    {
        _providers = providers;
    }

    public void Run()
    {
        while (true)
        {
            IEnumerable<IScenario> scenarios = GetScenarios();
            SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
                .Title("Select")
                .AddChoices(scenarios)
                .UseConverter(x => x.Name);

            IScenario scenario = AnsiConsole.Prompt(selector);
            scenario.Run();
        }
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
            {
                yield return scenario;
            }
        }
    }
}