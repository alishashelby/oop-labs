using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Deposit(AccountModel accountModel, decimal amount)
    {
        bool isSuccess = await _repository.AddTransaction(
                new TransactionModel(accountModel.Id, DateTime.Now, amount, TransactionType.Deposit))
            .ConfigureAwait(false);

        return isSuccess;
    }

    public async Task<bool> Withdraw(AccountModel accountModel, decimal amount)
    {
        bool isSuccess = await _repository.AddTransaction(
            new TransactionModel(accountModel.Id, DateTime.Now, amount, TransactionType.Withdraw))
            .ConfigureAwait(false);

        return isSuccess;
    }

    public IEnumerable<TransactionModel> GetHistory(int accountId)
    {
        return _repository.GetTransactionsByAccountId(accountId)
            .OrderByDescending(t => t.Date);
    }
}