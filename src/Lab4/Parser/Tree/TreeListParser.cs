using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;
using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser.Tree;

public class TreeListParser : CommandParserBase
{
    private readonly IStream _stream;

    public TreeListParser(IStream stream)
    {
        _stream = stream;
    }

    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        return args is [_, _] && FileSystem is not null
            ? new Success<ICommand>("Command created", new TreeListCommand(FileSystem, _stream))
            : args is [_, _, "-d", _] && Convert.ToInt32(args[3]) <= 0
            ? new Failure<ICommand>("Invalid command")
            : args is not [_, _, "-d", _]
            ? new Failure<ICommand>("Invalid command")
            : FileSystem is null
            ? new Failure<ICommand>("Not connected to fileSystem")
            : new Success<ICommand>("Command created", new TreeListCommand(FileSystem, _stream, Convert.ToInt32(args[3])));
    }
}