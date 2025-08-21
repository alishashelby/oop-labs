using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class ConnectCommand : ICommand
{
    private readonly string _address;
    private readonly IFileSystem _fileSystem;

    public ConnectCommand(string address, IFileSystem fileSystem)
    {
        _address = address;
        _fileSystem = fileSystem;
    }

    public IResultType<ICommand> Execute()
    {
        if (string.IsNullOrEmpty(_address))
        {
            return new Failure<ICommand>("Address is required");
        }

        IResultType<IFileSystem> result = _fileSystem.Connect(_address);
        return result is Failure<IFileSystem>
            ? new Failure<ICommand>(result.Message)
            : new Success<ICommand>("Connected to " + _address);
    }
}