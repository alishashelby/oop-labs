using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem.States;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem;

public interface IFileSystem
{
    string CurrentAddress { get; set; }

    void ChangeState(IState newState);

    IResultType<IFileSystem> Connect(string address);

    void Disconnect();

    IResultType<IFileSystem> GetToDirectory(string path);

    IResultType<IEnumerable<string>> GetDirectoryContentList(int depth, ITreeParameterizer parameterizer);

    IResultType<string> GetFileContent(string path);

    IResultType<IFileSystem> MoveFile(string sourcePath, string destinationPath);

    IResultType<IFileSystem> CopyFile(string sourcePath, string destinationPath);

    IResultType<IFileSystem> DeleteFile(string path);

    IResultType<IFileSystem> RenameFile(string path, string newName);
}