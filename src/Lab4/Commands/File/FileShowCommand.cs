using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.File;

public class FileShowCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly string _path;
    private readonly IStream _stream;

    public FileShowCommand(IFileSystem fileSystem, string path, IStream stream)
    {
        _fileSystem = fileSystem;
        _path = path;
        _stream = stream;
    }

    public IResultType<ICommand> Execute()
    {
        IResultType<string> result = _fileSystem.GetFileContent(_path);
        if (result is Failure<string> || result.Result is null)
        {
            return new Failure<ICommand>(result.Message);
        }

        _stream.Write(result.Result);
        return new Success<ICommand>("File has been shown from " + _path);
    }
}