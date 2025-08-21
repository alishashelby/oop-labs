using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.File;

public class FileDeleteCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _path;

    public FileDeleteCommand(IFileSystem fileSystem, string path)
    {
        _fileSystem = fileSystem;
        _path = path;
    }

    public IResultType<ICommand> Execute()
    {
        IResultType<IFileSystem> result = _fileSystem.DeleteFile(_path);
        return result is Failure<IFileSystem> || result.Result is null
            ? new Failure<ICommand>(result.Message)
            : new Success<ICommand>("File deleted.");
    }
}