using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.CreateAccount;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Deposit;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.GetBalance;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Login;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.ViewTransactionHistory;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.Client.UseAccount.Withdraw;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Scenarios.TurnOff;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ATMRunner>();

        collection.AddScoped<IScenarioProvider, AdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ClientScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ATMTurnOffProvider>();

        collection.AddScoped<ClientLoginScenario>();
        collection.AddScoped<CreateAccountScenario>();
        collection.AddScoped<BackScenario>();

        collection.AddScoped<IScenario, GetBalanceScenario>();
        collection.AddScoped<IScenario, DepositScenario>();
        collection.AddScoped<IScenario, WithdrawScenario>();
        collection.AddScoped<IScenario, TransactionHistoryScenario>();
        collection.AddScoped<IScenario, ExitAccountScenario>();

        return collection;
    }
}