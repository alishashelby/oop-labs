using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddScoped<ITransactionRepository, TransactionRepository>();

        return collection;
    }
}