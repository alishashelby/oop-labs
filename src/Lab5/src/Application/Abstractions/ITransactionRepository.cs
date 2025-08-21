using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;

public interface ITransactionRepository
{
    IEnumerable<TransactionModel> GetTransactionsByAccountId(int accountId);

    Task<bool> AddTransaction(TransactionModel transactionModel);
}