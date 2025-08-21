using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.States;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public class LocalFileSystem : IFileSystem
{
    private IState _state;

    public LocalFileSystem()
    {
        _state = new DisconnectedState(this);
    }

    public string CurrentAddress { get; set; } = string.Empty;

    public void ChangeState(IState newState)
    {
        _state = newState;
    }

    public IResultType<IFileSystem> Connect(string address)
    {
        return _state.Connect(address);
    }

    public void Disconnect()
    {
        _state.Disconnect();
    }

    public IResultType<IFileSystem> GetToDirectory(string path)
    {
        return _state.GetToDirectory(path);
    }

    public IResultType<IEnumerable<string>> GetDirectoryContentList(int depth, ITreeParameterizer parameterizer)
    {
        return _state.GetDirectoryContentList(depth, parameterizer);
    }

    public IResultType<string> GetFileContent(string path)
    {
        return _state.GetFileContent(path);
    }

    public IResultType<IFileSystem> MoveFile(string sourcePath, string destinationPath)
    {
        return _state.MoveFile(sourcePath, destinationPath);
    }

    public IResultType<IFileSystem> CopyFile(string sourcePath, string destinationPath)
    {
        return _state.CopyFile(sourcePath, destinationPath);
    }

    public IResultType<IFileSystem> DeleteFile(string path)
    {
        return _state.DeleteFile(path);
    }

    public IResultType<IFileSystem> RenameFile(string path, string newName)
    {
        return _state.RenameFile(path, newName);
    }
}