using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.File;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.File;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Moq;
using Xunit;

namespace Lab4.Tests.FileMove.UnitTests;

public class FileMoveParserTests
{
    [Fact]
    public void ParseCorrectCommandShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();

        var parser = new FileMoveParser();
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["file", "move", "/alishashelby/src/Lab4/Lab4.csproj", "/alishashelby/src/Lab4/bin"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<FileMoveCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWhenDisconnectedShouldReturnFailure()
    {
        // Arrange
        var parser = new FileMoveParser();

        string[] input = ["file", "move", "/alishashelby/src/Lab4/Lab4.csproj", "/alishashelby/src/Lab4/bin"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }
}