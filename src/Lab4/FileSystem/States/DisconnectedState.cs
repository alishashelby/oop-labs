using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System.Collections.Generic;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem.States;

public class DisconnectedState : IState
{
    private readonly IFileSystem _fileSystem;

    public DisconnectedState(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IResultType<IFileSystem> Connect(string address)
    {
        if (!Directory.Exists(address))
        {
            return new Failure<IFileSystem>("Directory does not exist");
        }

        _fileSystem.CurrentAddress = address;
        _fileSystem.ChangeState(new ConnectedState(_fileSystem));
        return new Success<IFileSystem>("Connect command was executed");
    }

    public void Disconnect() { }

    public IResultType<IFileSystem> GetToDirectory(string path)
    {
        return new Failure<IFileSystem>("Directory is not connected");
    }

    public IResultType<IEnumerable<string>> GetDirectoryContentList(int depth, ITreeParameterizer parameterizer)
    {
        return new Failure<IEnumerable<string>>("Directory is not connected");
    }

    public IResultType<string> GetFileContent(string path)
    {
        return new Failure<string>("Directory is not connected");
    }

    public IResultType<IFileSystem> MoveFile(string sourcePath, string destinationPath)
    {
        return new Failure<IFileSystem>("Directory is not connected");
    }

    public IResultType<IFileSystem> CopyFile(string sourcePath, string destinationPath)
    {
        return new Failure<IFileSystem>("Directory is not connected");
    }

    public IResultType<IFileSystem> DeleteFile(string path)
    {
        return new Failure<IFileSystem>("Directory is not connected");
    }

    public IResultType<IFileSystem> RenameFile(string path, string newName)
    {
        return new Failure<IFileSystem>("Directory is not connected");
    }
}