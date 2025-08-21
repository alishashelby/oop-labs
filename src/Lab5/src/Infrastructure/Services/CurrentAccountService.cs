using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;

public class CurrentAccountService : ICurrentAccountService
{
    public AccountModel? Account { get; set; }
}