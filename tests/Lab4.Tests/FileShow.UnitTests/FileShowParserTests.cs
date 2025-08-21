using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.File;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.File;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;
using Moq;
using Xunit;

namespace Lab4.Tests.FileShow.UnitTests;

public class FileShowParserTests
{
    [Fact]
    public void ParseCorrectCommandShouldReturnSuccessAndCommand()
    {
        // Arrange
        var mockStreamService = new Mock<IService<IStream>>();
        var mockStream = new Mock<IStream>();
        var mockFileSystem = new Mock<IFileSystem>();
        mockStreamService.Setup(service => service.GetInstance("console"))
            .Returns(new Success<IStream>("Mode found", mockStream.Object));

        var parser = new FileShowParser(mockStreamService.Object);
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["file", "show", "/alishashelby/src/Lab4/Lab4.csproj", "-m", "console"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Success<ICommand>);
        Assert.IsType<FileShowCommand>(parserResult.Result);
        Assert.NotNull(parserResult.Result);
        Assert.Equal(mockFileSystem.Object, parser.FileSystem);
    }

    [Fact]
    public void ParseCorrectCommandWhenDisconnectedShouldReturnFailure()
    {
        // Arrange
        var mockStreamService = new Mock<IService<IStream>>();
        var mockStream = new Mock<IStream>();
        mockStreamService.Setup(service => service.GetInstance("console"))
            .Returns(new Success<IStream>("Mode found", mockStream.Object));

        var parser = new FileShowParser(mockStreamService.Object);

        string[] input = ["file", "show", "/alishashelby/src/Lab4/Lab4.csproj", "-m", "console"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("There is no connection to a file system", parserResult.Message);
    }

    [Fact]
    public void ParseIncorrectCommandWithUnregisteredModeShouldReturnFailure()
    {
        // Arrange
        var mockStreamService = new Mock<IService<IStream>>();
        var mockFileSystem = new Mock<IFileSystem>();
        mockStreamService.Setup(service => service.GetInstance("notExistingMode"))
            .Returns(new Failure<IStream>("Unknown mode"));

        var parser = new FileShowParser(mockStreamService.Object);
        parser.SetFileSystem(mockFileSystem.Object);

        string[] input = ["file", "show", "/alishashelby/src/Lab4/Lab4.csproj", "-m", "notExistingMode"];

        // Act
        IResultType<ICommand> parserResult = parser.Parse(input);

        // Assert
        Assert.True(parserResult is Failure<ICommand>);
        Assert.Null(parserResult.Result);
        Assert.Equal("Unknown mode", parserResult.Message);
    }
}