using Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystem.States;

public class ConnectedState : IState
{
    private readonly IFileSystem _fileSystem;

    public ConnectedState(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public IResultType<IFileSystem> Connect(string address)
    {
        return new Failure<IFileSystem>("Already connected to file system");
    }

    public void Disconnect()
    {
        _fileSystem.CurrentAddress = string.Empty;
        _fileSystem.ChangeState(new DisconnectedState(_fileSystem));
    }

    public IResultType<IFileSystem> GetToDirectory(string path)
    {
        string newPath = Path.IsPathRooted(path)
            ? path
            : Path.Combine(_fileSystem.CurrentAddress, path);

        if (!Path.Exists(newPath))
        {
            return new Failure<IFileSystem>("Directory does not exist");
        }

        _fileSystem.CurrentAddress = newPath;
        return new Success<IFileSystem>("Directory executed");
    }

    public IResultType<IEnumerable<string>> GetDirectoryContentList(int depth, ITreeParameterizer parameterizer)
    {
        try
        {
            List<string> result = [];
            BuildTree(_fileSystem.CurrentAddress, parameterizer, result, depth, 0);
            return new Success<IEnumerable<string>>("Tree was built successfully", result);
        }
        catch (Exception ex)
        {
            return new Failure<IEnumerable<string>>(ex.Message);
        }
    }

    public IResultType<string> GetFileContent(string path)
    {
        string newPath = Path.IsPathRooted(path)
            ? path
            : Path.Combine(_fileSystem.CurrentAddress, path);

        return !Path.Exists(newPath)
            ? new Failure<string>("File does not exist")
            : new Success<string>("File executed", File.ReadAllText(newPath));
    }

    public IResultType<IFileSystem> MoveFile(string sourcePath, string destinationPath)
    {
        string newSourcePath = Path.IsPathRooted(sourcePath)
            ? sourcePath
            : Path.Combine(_fileSystem.CurrentAddress, sourcePath);
        string newDestinationPath = Path.IsPathRooted(destinationPath)
            ? destinationPath
            : Path.Combine(_fileSystem.CurrentAddress, destinationPath);

        if (!Path.Exists(newSourcePath))
        {
            return new Failure<IFileSystem>("File does not exist");
        }

        if (!Directory.Exists(newDestinationPath))
        {
            return new Failure<IFileSystem>("Directory does not exist");
        }

        string destinationFilePath = Path.Combine(newDestinationPath, Path.GetFileName(newSourcePath));
        if (File.Exists(destinationFilePath))
        {
            return new Failure<IFileSystem>($"The file '{Path.GetFileName(newSourcePath)}' already exists in the destination.");
        }

        try
        {
            File.Move(newSourcePath, destinationFilePath);
        }
        catch (Exception ex)
        {
            return new Failure<IFileSystem>(ex.Message);
        }

        return new Success<IFileSystem>("Move command was executed");
    }

    public IResultType<IFileSystem> CopyFile(string sourcePath, string destinationPath)
    {
        string newSourcePath = Path.IsPathRooted(sourcePath)
            ? sourcePath
            : Path.Combine(_fileSystem.CurrentAddress, sourcePath);
        string newDestinationPath = Path.IsPathRooted(destinationPath)
            ? destinationPath
            : Path.Combine(_fileSystem.CurrentAddress, destinationPath);

        if (!File.Exists(newSourcePath))
        {
            return new Failure<IFileSystem>("File does not exist");
        }

        if (!Directory.Exists(newDestinationPath))
        {
            return new Failure<IFileSystem>("Directory does not exist");
        }

        string destinationFilePath = Path.Combine(newDestinationPath, Path.GetFileName(newSourcePath));
        if (File.Exists(destinationFilePath))
        {
            return new Failure<IFileSystem>($"The file '{Path.GetFileName(newSourcePath)}' already exists in the destination.");
        }

        try
        {
            File.Copy(newSourcePath, destinationFilePath);
        }
        catch (Exception ex)
        {
            return new Failure<IFileSystem>(ex.Message);
        }

        return new Success<IFileSystem>("Copy command was executed");
    }

    public IResultType<IFileSystem> DeleteFile(string path)
    {
        string newPath = Path.IsPathRooted(path)
            ? path
            : Path.Combine(_fileSystem.CurrentAddress, path);

        if (!File.Exists(newPath))
        {
            return new Failure<IFileSystem>("File does not exist");
        }

        try
        {
            File.Delete(newPath);
        }
        catch (Exception ex)
        {
            return new Failure<IFileSystem>(ex.Message);
        }

        return new Success<IFileSystem>("Delete command was executed");
    }

    public IResultType<IFileSystem> RenameFile(string path, string newName)
    {
        string newPath = Path.IsPathRooted(path)
            ? path
            : Path.Combine(_fileSystem.CurrentAddress, path);

        if (!File.Exists(newPath))
        {
            return new Failure<IFileSystem>("File does not exist");
        }

        string? directoryPath = Path.GetDirectoryName(newPath);
        if (directoryPath is null)
        {
            return new Failure<IFileSystem>("Directory does not exist");
        }

        string newFilePath = Path.Combine(directoryPath, newName);
        try
        {
            File.Move(path, newFilePath);
        }
        catch (Exception ex)
        {
            return new Failure<IFileSystem>(ex.Message);
        }

        return new Success<IFileSystem>("Rename command was executed");
    }

    private static void BuildTree(
        string path,
        ITreeParameterizer parameterizer,
        List<string> result,
        int maxDepth,
        int curDepth)
    {
        if (curDepth >= maxDepth)
        {
            return;
        }

        string[] directories = Directory.GetDirectories(path);
        string[] files = Directory.GetFiles(path);

        foreach (string directory in directories)
        {
            string folderName = Path.GetFileName(directory);
            result.Add($"{new string(
                parameterizer.IndentationOption[0],
                curDepth * parameterizer.IndentationOption.Length)}" +
                       $"{parameterizer.FolderOption} {folderName}");

            BuildTree(directory, parameterizer, result, maxDepth, curDepth + 1);
        }

        result.AddRange(files.Select(Path.GetFileName)
            .Select(fileName =>
                $"{new string(
                    parameterizer.IndentationOption[0],
                    curDepth * parameterizer.IndentationOption.Length)}" +
                $"{parameterizer.FileOption} {fileName}"));
    }
}