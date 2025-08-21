using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser.Tree;

public class TreeGoToParser : CommandParserBase
{
    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        return args.Length != 3
            ? new Failure<ICommand>("Invalid command")
            : FileSystem is null
            ? new Failure<ICommand>("Not connected to fileSystem")
            : new Success<ICommand>("Command created", new TreeGoToCommand(args[2], FileSystem));
    }
}