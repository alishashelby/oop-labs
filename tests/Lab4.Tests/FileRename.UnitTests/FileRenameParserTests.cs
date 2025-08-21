using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.File;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.File;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Moq;
using Xunit;

namespace Lab4.Tests.FileRename.UnitTests;

public class FileRenameParserTests
{
    [Fact]
    public void ParseCorrectCommandShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockFileSystem = new Mock<IFileSystem>();

        var parser = new FileRenameParser();
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["file", "rename", "/alishashelby/src/Lab4/Lab4.csproj", "new.csproj"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<FileRenameCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWhenDisconnectedShouldReturnFailure()
    {
        // Arrange
        var parser = new FileRenameParser();

        string[] input = ["file", "rename", "/alishashelby/src/Lab4/Lab4.csproj", "new.csproj"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }
}