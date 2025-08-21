using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;

public interface IAccountRepository
{
    AccountModel? FindAccountById(int id);

    Task CreateAccount(AccountModel accountModel);

    Task UpdateBalance(AccountModel accountModel);
}