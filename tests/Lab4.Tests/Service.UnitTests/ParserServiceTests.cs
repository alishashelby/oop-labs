using Itmo.ObjectOrientedProgramming.Lab4.Parser;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;
using Moq;
using System;
using Xunit;

namespace Lab4.Tests.Service.UnitTests;

public class ParserServiceTests
{
    [Fact]
    public void AddNewParserShouldSaveIt()
    {
        // Arrange
        var parserService = new ParserService();
        var mockParser = new Mock<CommandParserBase>();

        // Act
        parserService.Add("name of command", mockParser.Object);

        // Assert
        IResultType<CommandParserBase> result = parserService.GetInstance("name of command");
        Assert.True(result is Success<CommandParserBase>);
        Assert.Equal(mockParser.Object, result.Result);
    }

    [Fact]
    public void AddSameParserAgainShouldThrowException()
    {
        // Arrange
        var parserService = new ParserService();
        var firstMockParser = new Mock<CommandParserBase>();
        var secondMockParser = new Mock<CommandParserBase>();

        // Act
        parserService.Add("same name of command", firstMockParser.Object);

        // Assert
        Exception exception = Assert.Throws<Exception>(() =>
            parserService.Add("same name of command", secondMockParser.Object));
        Assert.Equal("Command same name of command is already registered", exception.Message);
    }

    [Fact]
    public void GetInstanceOfRegisteredParserShouldReturnSuccessAndInstance()
    {
        // Arrange
        var parserService = new ParserService();
        var mockParser = new Mock<CommandParserBase>();
        parserService.Add("name of command", mockParser.Object);

        // Act
        IResultType<CommandParserBase> result = parserService.GetInstance("name of command");

        // Assert
        Assert.True(result is Success<CommandParserBase>);
        Assert.Equal(mockParser.Object, result.Result);
    }

    [Fact]
    public void GetInstanceOfUnregisteredParserShouldReturnFailure()
    {
        // Arrange
        var parserService = new ParserService();

        // Act
        IResultType<CommandParserBase> result = parserService.GetInstance("name of command");

        // Assert
        Assert.True(result is Failure<CommandParserBase>);
        Assert.Null(result.Result);
        Assert.Equal("Command not found", result.Message);
    }
}