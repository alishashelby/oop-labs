using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Moq;
using Xunit;

namespace Lab4.Tests.TreeGoTo.UnitTests;

public class TreeGoToParserTests
{
    [Fact]
    public void ParseCorrectCommandShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();

        var parser = new TreeGoToParser();
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["tree", "goto", "/alishashelby/src/Lab4"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<TreeGoToCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWhenDisconnectedShouldReturnFailure()
    {
        // Arrange
        var parser = new TreeGoToParser();

        string[] input = ["tree", "goto", "/alishashelby/src/Lab4"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }
}