using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Commands.File;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser.File;

public class FileShowParser : CommandParserBase
{
    private readonly IService<IStream> _service;

    public FileShowParser(IService<IStream> service)
    {
        _service = service;
    }

    protected override IResultType<ICommand> CreateCommand(string[] args)
    {
        if (args is not [_, _, _, "-m", _])
        {
            return new Failure<ICommand>("Invalid command");
        }

        IResultType<IStream> result = _service.GetInstance(args[4]);
        if (result is Failure<IStream> || result.Result is null)
        {
            return new Failure<ICommand>(result.Message);
        }

        return FileSystem is null
            ? new Failure<ICommand>("Not connected to fileSystem")
            : new Success<ICommand>("Command created", new FileShowCommand(FileSystem, args[2], result.Result));
    }
}