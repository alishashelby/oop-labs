using System;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.TurnOff;

public class ATMTurnOff : IScenario
{
    public string Name => "Turn off ATM";

    public void Run()
    {
        Console.WriteLine("ATM turned off");
        Environment.Exit(0);
    }
}