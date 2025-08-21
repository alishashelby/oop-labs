using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Account;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Transaction;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Lab5.Tests.Deposit.UnitTests;

public class DepositUnitTests
{
    [Fact]
    public async Task DepositAmountReturnsSuccessAndUpdatesBalance()
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

        const decimal depositAmount = 100;
        mockTransactionService.Setup(
            x =>
                x.Deposit(It.IsAny<AccountModel>(), It.IsAny<decimal>())).ReturnsAsync(true);

        // Act
        TransactionResult result = await accountService.Deposit(depositAmount).ConfigureAwait(true);

        // Assert
        Assert.IsType<TransactionResult.Success>(result);
        Assert.Equal(200, account.Balance);
        mockAccountRepository.Verify(
            x => x.UpdateBalance(It.IsAny<AccountModel>()), Times.Once);
    }
}