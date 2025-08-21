using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;
using System;
using System.Collections.Concurrent;

namespace Itmo.ObjectOrientedProgramming.Lab4.Service;

public class OutputService : IService<IStream>
{
    private readonly ConcurrentDictionary<string, IStream> _streams = [];

    public void Add(string name, IStream instance)
    {
        if (!_streams.TryAdd(name, instance))
            throw new Exception($"Command {name} is already registered");
    }

    public IResultType<IStream> GetInstance(string name)
    {
        return !_streams.TryGetValue(name, out IStream? stream)
            ? new Failure<IStream>("Command not found")
            : new Success<IStream>("Command found", stream);
    }
}