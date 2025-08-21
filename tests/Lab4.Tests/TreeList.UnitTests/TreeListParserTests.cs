using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;
using Moq;
using Xunit;

namespace Lab4.Tests.TreeList.UnitTests;

public class TreeListParserTests
{
    [Fact]
    public void ParseCorrectCommandWithNoFlagShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();
        var mockStream = new Mock<IStream>();

        var parser = new TreeListParser(mockStream.Object);
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["tree", "list"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<TreeListCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWithFlagShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();
        var mockStream = new Mock<IStream>();

        var parser = new TreeListParser(mockStream.Object);
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["tree", "list", "-d", "2"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<TreeListCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWithNegativeFlagShouldReturnFailure()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();
        var mockStream = new Mock<IStream>();

        var parser = new TreeListParser(mockStream.Object);
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["tree", "list", "-d", "-1"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("Invalid command", parserResult.Message);
    }

    [Fact]
    public void ParseCorrectCommandWhenDisconnectedShouldReturnFailure()
    {
        // Arrange
        var mockStream = new Mock<IStream>();
        var parser = new TreeListParser(mockStream.Object);

        string[] input = ["tree", "list", "-d", "2"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }
}