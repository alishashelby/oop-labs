namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios;

public interface IScenario
{
    string Name { get; }

    void Run();
}