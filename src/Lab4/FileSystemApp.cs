using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystem;
using Itmo.ObjectOrientedProgramming.Lab4.Parser;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.File;
using Itmo.ObjectOrientedProgramming.Lab4.Parser.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.ResultType;
using Itmo.ObjectOrientedProgramming.Lab4.Service;
using Itmo.ObjectOrientedProgramming.Lab4.Stream;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class FileSystemApp
{
    private readonly ParserService _parserService;
    private readonly IStream _stream;
    private IFileSystem? _fileSystem;

    public FileSystemApp(IStream stream)
    {
        _stream = stream;

        var outputStreamService = new OutputService();
        outputStreamService.Add("console", new ConsoleStream());

        var systemService = new FileSystemService();
        systemService.Add("local", new LocalFileSystem());

        _parserService = new ParserService();
        _parserService.Add("connect", new ConnectParser(systemService));
        _parserService.Add("disconnect", new DisconnectParser());
        _parserService.Add("tree goto", new TreeGoToParser());
        _parserService.Add("tree list", new TreeListParser(_stream));
        _parserService.Add("file show", new FileShowParser(outputStreamService));
        _parserService.Add("file move", new FileMoveParser());
        _parserService.Add("file copy", new FileCopyParser());
        _parserService.Add("file delete", new FileDeleteParser());
        _parserService.Add("file rename", new FileRenameParser());
    }

    public void Run()
    {
        while (true)
        {
            string input = _stream.GetInput();
            IResultType<FileSystemApp> rez = RunCommand(input);
            _stream.Write(rez.Message);
        }
    }

    private IResultType<FileSystemApp> RunCommand(string input)
    {
        string[] arguments = input.Split(" ");

        IResultType<CommandParserBase> findParserResult = FindParser(arguments);
        if (findParserResult is Failure<CommandParserBase> failure)
        {
            return new Failure<FileSystemApp>(failure.Message);
        }

        CommandParserBase? parser = findParserResult.Result;
        if (parser is null)
        {
            return new Failure<FileSystemApp>("Parser not found");
        }

        parser.SetFileSystem(_fileSystem);

        IResultType<ICommand> parserResult = parser.Parse(arguments);
        if (parserResult is Failure<ICommand>)
        {
            return new Failure<FileSystemApp>(parserResult.Message);
        }

        ICommand? command = parserResult.Result;
        if (command is null)
        {
            return new Failure<FileSystemApp>("Command not found");
        }

        IResultType<ICommand> commandResult = command.Execute();
        if (commandResult is Failure<ICommand>)
        {
            return new Failure<FileSystemApp>(commandResult.Message);
        }

        _fileSystem = command switch
        {
            ConnectCommand when commandResult is Success<ICommand> => parser.FileSystem,
            DisconnectCommand when commandResult is Success<ICommand> => null,
            _ => _fileSystem,
        };

        return new Success<FileSystemApp>(commandResult.Message);
    }

    private IResultType<CommandParserBase> FindParser(string[] arguments)
    {
        string commandName = arguments[0];
        IResultType<CommandParserBase> commandResult = _parserService.GetInstance(commandName);
        if (commandResult is Failure<CommandParserBase> && arguments.Length > 1)
        {
            commandResult = _parserService.GetInstance($"{commandName} {arguments[1]}");
        }

        return commandResult;
    }
}