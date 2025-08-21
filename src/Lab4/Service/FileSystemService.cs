using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System;
using System.Collections.Concurrent;

namespace Itmo.ObjectOrientedProgramming.Lab4.Service;

public class FileSystemService : IService<IFileSystem>
{
    private readonly ConcurrentDictionary<string, IFileSystem> _modes = [];

    public void Add(string name, IFileSystem instance)
    {
        if (!_modes.TryAdd(name, instance))
            throw new Exception($"Mode {name} is already registered");
    }

    public IResultType<IFileSystem> GetInstance(string name)
    {
        return !_modes.TryGetValue(name, out IFileSystem? fileSystem)
            ? new Failure<IFileSystem>("Unknown mode")
            : new Success<IFileSystem>("Mode found", fileSystem);
    }
}