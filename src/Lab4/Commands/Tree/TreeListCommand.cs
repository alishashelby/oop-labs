using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;
using System;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree;

public class TreeListCommand : ICommand
{
    private readonly IFileSystem _fileSystem;
    private readonly IStream _stream;
    private readonly int _depth;

    public TreeListCommand(IFileSystem fileSystem, IStream stream, int depth = 1)
    {
        _fileSystem = fileSystem;
        _depth = depth;
        _stream = stream;
    }

    public IResultType<ICommand> Execute()
    {
        ITreeParameterizer parameterizer;
        try
        {
            parameterizer = ConfigTreeParameterizer.Update();
        }
        catch (Exception ex)
        {
            return new Failure<ICommand>(ex.Message);
        }

        IResultType<IEnumerable<string>> result = _fileSystem.GetDirectoryContentList(_depth, parameterizer);
        if (result is Failure<IEnumerable<string>> || result.Result is null)
        {
            return new Failure<ICommand>(result.Message);
        }

        foreach (string line in result.Result)
        {
            _stream.Write(line);
        }

        return new Success<ICommand>("Tree list successfully retrieved.");
    }
}