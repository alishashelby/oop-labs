using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.File;

public class FileRenameCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _path;
    private readonly string _newName;

    public FileRenameCommand(IFileSystem fileSystem, string path, string newName)
    {
        _fileSystem = fileSystem;
        _path = path;
        _newName = newName;
    }

    public IResultType<ICommand> Execute()
    {
        IResultType<IFileSystem> result = _fileSystem.RenameFile(_path, _newName);
        return result is Failure<IFileSystem> || result.Result is null
            ? new Failure<ICommand>(result.Message)
            : new Success<ICommand>("File renamed.");
    }
}