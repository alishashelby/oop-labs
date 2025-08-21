using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;

public interface ITransactionService
{
    Task<bool> Deposit(Models.AccountModel accountModel, decimal amount);

    Task<bool> Withdraw(Models.AccountModel accountModel, decimal amount);

    IEnumerable<Models.TransactionModel> GetHistory(int accountId);
}