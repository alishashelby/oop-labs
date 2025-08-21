namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;

public interface ICurrentAccountService
{
    Models.AccountModel? Account { get; set; }
}