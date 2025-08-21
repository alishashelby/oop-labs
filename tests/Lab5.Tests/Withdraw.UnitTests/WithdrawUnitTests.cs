using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Lab5.Tests.Withdraw.UnitTests;

public class WithdrawUnitTests
{
    [Fact]
    public async Task WithdrawSufficientBalanceReturnsSuccessAndUpdatesBalance()
    {
        // Arrange
        var account = new AccountModel(0, "1234", 100);

        var mockAccountRepository = new Mock<IAccountRepository>();

        var mockCurrentAccountService = new Mock<ICurrentAccountService>();
        mockCurrentAccountService.Setup(x => x.Account).Returns(account);

        var mockTransactionService = new Mock<ITransactionService>();
        var accountService = new AccountService(
            mockAccountRepository.Object,
            mockCurrentAccountService.Object,
            mockTransactionService.Object);

        const decimal withdrawAmount = 100;
        mockTransactionService.Setup(
            x =>
                x.Withdraw(It.IsAny<AccountModel>(), It.IsAny<decimal>())).ReturnsAsync(true);

        // Act
        TransactionResult result = await accountService.Withdraw(withdrawAmount).ConfigureAwait(true);

        // Assert
        Assert.IsType<TransactionResult.Success>(result);
        Assert.Equal(0, account.Balance);
        mockAccountRepository.Verify(
            x => x.UpdateBalance(It.IsAny<AccountModel>()), Times.Once);
    }

    [Fact]
    public async Task WithdrawInSufficientBalanceReturnsFailure()
    {
        // Arrange
        var account = new AccountModel(0, "1234", 100);

        var mockAccountRepository = new Mock<IAccountRepository>();

        var mockCurrentAccountService = new Mock<ICurrentAccountService>();
        mockCurrentAccountService.Setup(x => x.Account).Returns(account);

        var mockTransactionService = new Mock<ITransactionService>();
        var accountService = new AccountService(
            mockAccountRepository.Object,
            mockCurrentAccountService.Object,
            mockTransactionService.Object);

        const decimal withdrawAmount = 101;
        mockTransactionService.Setup(
            x =>
                x.Withdraw(It.IsAny<AccountModel>(), It.IsAny<decimal>())).ReturnsAsync(true);

        // Act
        TransactionResult result = await accountService.Withdraw(withdrawAmount).ConfigureAwait(true);

        // Assert
        TransactionResult.Failure failure = Assert.IsType<TransactionResult.Failure>(result);
        Assert.Equal("Insufficient balance.", failure.Message);
        Assert.Equal(100, account.Balance);
        mockAccountRepository.Verify(
            x => x.UpdateBalance(It.IsAny<AccountModel>()), Times.Never);
    }
}