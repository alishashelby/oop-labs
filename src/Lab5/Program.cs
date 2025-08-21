using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5;

public class Program
{
    private static void Main(string[] args)
    {
        var collection = new ServiceCollection();

        collection
            .AddApplication()
            .AddInfrastructureDataAccess(configuration =>
            {
                configuration.Host = "localhost";
                configuration.Port = 5432;
                configuration.Username = "postgres";
                configuration.Password = "postgres";
                configuration.Database = "postgres";
                configuration.SslMode = "Prefer";
            })
            .AddPresentationConsole();
        ServiceProvider provider = collection.BuildServiceProvider();
        using IServiceScope scope = provider.CreateScope();

        scope.UseInfrastructureDataAccess();

        ATMRunner atmRunner = scope.ServiceProvider.GetRequiredService<ATMRunner>();

        while (true)
        {
            atmRunner.Run();
            AnsiConsole.Clear();
        }
    }
}