using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentAccountService _currentAccountService;
    private readonly ITransactionService _transactionService;

    public AccountService(
        IAccountRepository accountRepository,
        ICurrentAccountService currentAccountService,
        ITransactionService transactionService)
    {
        _accountRepository = accountRepository;
        _currentAccountService = currentAccountService;
        _transactionService = transactionService;
    }

    public async Task<LoginResult> RegisterAccount(int accountId, string pin)
    {
        if (pin.IsNullOrEmpty())
        {
            return new LoginResult.InvalidPin();
        }

        var newAccount = new AccountModel(accountId, Hasher.Hash(pin), 0);
        try
        {
            await _accountRepository.CreateAccount(newAccount).ConfigureAwait(false);

            return new LoginResult.Success();
        }
        catch (Exception)
        {
            return new LoginResult.Failure();
        }
    }

    public LoginResult Login(int accountId, string pin)
    {
        AccountModel? account = _accountRepository.FindAccountById(accountId);
        if (account is null || account.Pin == null || !Hasher.Verify(pin, account.Pin))
        {
            return new LoginResult.Failure();
        }

        _currentAccountService.Account = account;
        return new LoginResult.Success();
    }

    public decimal GetBalance()
    {
        return _currentAccountService.Account is null
            ? throw new InvalidOperationException("Current account is null.")
            : _currentAccountService.Account.Balance;
    }

    public async Task<TransactionResult> Deposit(decimal amount)
    {
        if (_currentAccountService.Account == null)
        {
            throw new InvalidOperationException("Current account is null.");
        }

        if (amount <= 0)
        {
            return new TransactionResult.Failure("Deposit amount must be greater than 0.");
        }

        bool isSuccess = await _transactionService
            .Deposit(_currentAccountService.Account, amount)
            .ConfigureAwait(false);
        if (!isSuccess)
        {
            return new TransactionResult.Failure("Deposit failed.");
        }

        _currentAccountService.Account.Balance += amount;

        try
        {
            await _accountRepository
                .UpdateBalance(_currentAccountService.Account)
                .ConfigureAwait(false);

            return new TransactionResult.Success();
        }
        catch (Exception ex)
        {
            return new TransactionResult.Failure(ex.Message);
        }
    }

    public async Task<TransactionResult> Withdraw(decimal amount)
    {
        if (_currentAccountService.Account == null)
        {
            throw new InvalidOperationException("Current account is null.");
        }

        if (amount <= 0)
        {
            return new TransactionResult.Failure("Withdraw amount must be greater than 0.");
        }

        if (_currentAccountService.Account.Balance < amount)
        {
            return new TransactionResult.Failure("Insufficient balance.");
        }

        bool isSuccess = await _transactionService
            .Withdraw(_currentAccountService.Account, amount)
            .ConfigureAwait(false);

        if (!isSuccess)
        {
            return new TransactionResult.Failure("Withdrawal failed.");
        }

        _currentAccountService.Account.Balance -= amount;

        try
        {
            await _accountRepository
                .UpdateBalance(_currentAccountService.Account)
                .ConfigureAwait(false);

            return new TransactionResult.Success();
        }
        catch (Exception ex)
        {
            return new TransactionResult.Failure(ex.Message);
        }
    }

    public IEnumerable<TransactionModel> GetHistory()
    {
        return _currentAccountService.Account == null
            ? throw new InvalidOperationException("Current account is null.")
            : _transactionService.GetHistory(_currentAccountService.Account.Id);
    }
}