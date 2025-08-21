using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser;

public class DisconnectParser : CommandParserBase
{
    protected override bool IsConnectedToFileSystem()
    {
        return false;
    }

    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        if (args.Length != 1)
        {
            return new Failure<ICommand>("Invalid command");
        }

        return FileSystem is null
            ? new Failure<ICommand>("Not connected to fileSystem")
            : new Success<ICommand>("Command created", new DisconnectCommand(FileSystem));
    }
}