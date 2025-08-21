using Itmo.ObjectOrientedProgramming.Lab4.Parser;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System;
using System.Collections.Concurrent;

namespace Itmo.ObjectOrientedProgramming.Lab4.Service;

public class ParserService : IService<CommandParserBase>
{
    private readonly ConcurrentDictionary<string, CommandParserBase> _commandparsers = [];

    public void Add(string name, CommandParserBase instance)
    {
        if (!_commandparsers.TryAdd(name, instance))
            throw new Exception($"Command {name} is already registered");
    }

    public IResultType<CommandParserBase> GetInstance(string name)
    {
        return !_commandparsers.TryGetValue(name, out CommandParserBase? parser)
            ? new Failure<CommandParserBase>("Command not found")
            : new Success<CommandParserBase>("Command found", parser);
    }
}