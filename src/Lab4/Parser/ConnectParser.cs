using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser;

public class ConnectParser : CommandParserBase
{
    private readonly IService<IFileSystem> _service;

    public ConnectParser(IService<IFileSystem> service)
    {
        _service = service;
    }

    protected override bool IsConnectedToFileSystem()
    {
        return true;
    }

    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        if (args is not [_, _, "-m", _])
        {
            return new Failure<ICommand>("Invalid command");
        }

        IResultType<IFileSystem> result = _service.GetInstance(args[3]);
        if (result is Failure<IFileSystem>)
        {
            return new Failure<ICommand>(result.Message);
        }

        FileSystem = result.Result;
        return FileSystem is null
            ? new Failure<ICommand>("FileSystem could not be found")
            : new Success<ICommand>("Command created", new ConnectCommand(args[1], FileSystem));
    }
}