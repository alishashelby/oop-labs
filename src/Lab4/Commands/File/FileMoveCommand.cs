using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.File;

public class FileMoveCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(IFileSystem fileSystem, string sourcePath, string destinationPath)
    {
        _fileSystem = fileSystem;
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public IResultType<ICommand> Execute()
    {
        IResultType<IFileSystem> result = _fileSystem.MoveFile(_sourcePath, _destinationPath);
        return result is Failure<IFileSystem> || result.Result is null
            ? new Failure<ICommand>(result.Message)
            : new Success<ICommand>("File moved.");
    }
}