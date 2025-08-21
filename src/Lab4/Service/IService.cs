using Itmo.ObjectOrientedProgramming.Lab4.ResultType;

namespace Itmo.ObjectOrientedProgramming.Lab4.Service;

public interface IService<T>
{
    void Add(string name, T instance);

    IResultType<T> GetInstance(string name);
}