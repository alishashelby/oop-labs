using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<TransactionType>("transaction_type");
    }
}