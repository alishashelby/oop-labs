using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Admin;
using Spectre.Console;
using System;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Admin;

public class AdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Admin";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter pin: ");
        LoginResult result = _adminService.Login(pin);

        if (result is LoginResult.Success)
        {
            AnsiConsole.MarkupLine("[green]You logged in.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Invalid pin![/]");
            Environment.Exit(0);
        }

        AnsiConsole.Ask<string>("Ok");
        AnsiConsole.Clear();
    }
}