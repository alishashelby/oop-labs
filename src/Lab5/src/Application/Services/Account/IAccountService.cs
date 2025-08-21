using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;

public interface IAccountService
{
    Task<LoginResult> RegisterAccount(int accountId, string pin);

    LoginResult Login(int accountId, string pin);

    decimal GetBalance();

    Task<TransactionResult> Deposit(decimal amount);

    Task<TransactionResult> Withdraw(decimal amount);

    IEnumerable<Models.TransactionModel> GetHistory();
}