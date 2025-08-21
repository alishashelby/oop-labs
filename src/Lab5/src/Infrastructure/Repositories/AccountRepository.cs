using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public AccountModel? FindAccountById(int id)
    {
        const string sql = """
                           select account_id, pin, balance
                           from account_balance
                           where account_id = @account_id
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_id", id);
        using NpgsqlDataReader reader = command.ExecuteReader();

        return reader.Read() is false
            ? null
            : new AccountModel(
                id: reader.GetInt32(0),
                pin: reader.GetString(1),
                balance: reader.GetDecimal(2));
    }

    public async Task CreateAccount(AccountModel accountModel)
    {
        const string sql = """
                           insert into account_balance (account_id, pin, balance) 
                           values (@account_id, @pin, @balance)
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("account_id", accountModel.Id);
        command.Parameters.AddWithValue("pin", accountModel.Pin ?? throw new InvalidOperationException());
        command.Parameters.AddWithValue("balance", accountModel.Balance);

        await command.ExecuteNonQueryAsync().ConfigureAwait(true);
    }

    public async Task UpdateBalance(AccountModel accountModel)
    {
        const string sql = """
                            update account_balance
                            set balance = @balance
                            where account_id = @account_id
                            """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("balance", accountModel.Balance);
        command.Parameters.AddWithValue("account_id", accountModel.Id);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }
}