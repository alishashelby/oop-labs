using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Moq;
using Xunit;

namespace Lab4.Tests.Disconnect.UnitTests;

public class DisconnectParserTests
{
    [Fact]
    public void ParseCorrectCommandWhenConnectedShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();

        var parser = new DisconnectParser();
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["disconnect"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<DisconnectCommand>(parserResult.Result);
        Assert.NotNull(parser.FileSystem);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWhenAlreadyDisconnectedShouldReturnFailure()
    {
        // Arrange
        var parser = new DisconnectParser();

        string[] input = ["disconnect"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parser.FileSystem);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }
}