using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<TransactionModel> GetTransactionsByAccountId(int accountId)
    {
        const string sql = """
                           select transaction_id, account_id, amount, type, date_time
                           from transactions
                           where account_id = @account_id
                           order by date_time desc
                           """;
        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default).AsTask().GetAwaiter().GetResult();
        using NpgsqlCommand command = new NpgsqlCommand(sql, connection).AddParameter("account_id", accountId);
        using NpgsqlDataReader reader = command.ExecuteReader();

        var transactions = new List<TransactionModel>();

        while (reader.Read())
        {
            decimal amount = reader.GetDecimal(2);
            var type = (TransactionType)reader.GetValue(3);
            DateTime dateTime = reader.GetDateTime(4);

            transactions.Add(new TransactionModel(accountId, dateTime, amount, type));
        }

        return transactions;
    }

    public async Task<bool> AddTransaction(TransactionModel transactionModel)
    {
        const string sqlTransaction = """
                                      insert into transactions (transaction_id, account_id, amount, type, date_time)
                                      values (@transaction_id, @account_id, @amount, @type, @date_time)
                                      """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default).ConfigureAwait(false);
        using NpgsqlTransaction newTransaction = await connection.BeginTransactionAsync().ConfigureAwait(false);

        try
        {
            using var commandTransaction = new NpgsqlCommand(sqlTransaction, connection, newTransaction);
            commandTransaction.Parameters.AddWithValue("transaction_id", Guid.NewGuid());
            commandTransaction.Parameters.AddWithValue("account_id", transactionModel.AccountId);
            commandTransaction.Parameters.AddWithValue("amount", transactionModel.Amount);
            commandTransaction.Parameters.AddWithValue("type", transactionModel.Type);
            commandTransaction.Parameters.AddWithValue("date_time", transactionModel.Date);

            await commandTransaction.ExecuteNonQueryAsync().ConfigureAwait(false);

            await newTransaction.CommitAsync().ConfigureAwait(false);
            return true;
        }
        catch (Exception)
        {
            await newTransaction.RollbackAsync().ConfigureAwait(false);
            return false;
        }
    }
}