using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.File;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser.File;

public class FileMoveParser : CommandParserBase
{
    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        return args.Length != 4
            ? new Failure<ICommand>("Invalid command")
            : FileSystem is null
            ? new Failure<ICommand>("Not connected to fileSystem")
            : new Success<ICommand>("Command created", new FileMoveCommand(FileSystem, args[2], args[3]));
    }
}