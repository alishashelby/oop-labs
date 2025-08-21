using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parser;

public abstract class CommandParserBase
{
    public IFileSystem? FileSystem { get; protected set; }

    public IResultType<ICommand> Parse(string[] args)
    {
        if (FileSystem == null && !IsConnectedToFileSystem())
        {
            return new Failure<ICommand>("There is no connection to a file system");
        }

        IResultType<ICommand> result = CreateCommand(args);
        return result;
    }

    public void SetFileSystem(IFileSystem? fileSystem)
    {
        FileSystem = fileSystem;
    }

    protected virtual bool IsConnectedToFileSystem()
    {
        return false;
    }

    protected abstract IResultType<ICommand> CreateCommand(string[] args);
}