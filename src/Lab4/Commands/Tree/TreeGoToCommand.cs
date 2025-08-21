using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;

public class TreeGoToCommand : ICommand
{
    private readonly string _path;
    private readonly IFileSystem _fileSystem;

    public TreeGoToCommand(string path, IFileSystem fileSystem)
    {
        _path = path;
        _fileSystem = fileSystem;
    }

    public IResultType<ICommand> Execute()
    {
        IResultType<IFileSystem> result = _fileSystem.GetToDirectory(_path);
        return result is Failure<IFileSystem>
            ? new Failure<ICommand>(result.Message)
            : new Success<ICommand>("Went to " + _path);
    }
}