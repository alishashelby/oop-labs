using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;
using Moq;
using Xunit;

namespace Lab4.Tests.Connect.UnitTests;

public class ConnectParserTests
{
    [Fact]
    public void ParseCorrectCommandShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockSystemService = new Mock<IService<IFileSystem>>();
        var mockFileSystem = new Mock<IFileSystem>();
        mockSystemService.Setup(service => service.GetInstance("local"))
            .Returns(new Success<IFileSystem>("Mode found", mockFileSystem.Object));

        var parser = new ConnectParser(mockSystemService.Object);
        string[] input = ["connect", "/alishashelby/src/Lab4", "-m", "local"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<ConnectCommand>(parserResult.Result);

        Assert.NotNull(parser.FileSystem);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseIncorrectCommandWithUnregisteredModeShouldReturnFailure()
    {
        // Arrange
        var mockSystemService = new Mock<IService<IFileSystem>>();
        mockSystemService.Setup(service => service.GetInstance("notExistingMode"))
            .Returns(new Failure<IFileSystem>("Unknown mode"));

        var parser = new ConnectParser(mockSystemService.Object);
        string[] input = ["connect", "/alishashelby/src/Lab4", "-m", "notExistingMode"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);

        Assert.Null(parserResult.Result);
        Assert.Equal("Unknown mode", parserResult.Message);
    }
}