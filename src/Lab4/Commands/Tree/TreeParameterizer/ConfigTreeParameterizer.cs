using System.Configuration;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands.Tree.TreeParameterizer;

public class ConfigTreeParameterizer : ITreeParameterizer
{
    public string FolderOption { get; }

    public string FileOption { get; }

    public string IndentationOption { get; }

    private ConfigTreeParameterizer(string folderOption, string fileOption, string indentationOption)
    {
        FolderOption = folderOption;
        FileOption = fileOption;
        IndentationOption = indentationOption;
    }

    public static ITreeParameterizer Update()
    {
        string fileOption = ConfigurationManager.AppSettings["FileOption"] ?? "📃";
        string folderOption = ConfigurationManager.AppSettings["FolderOption"] ?? "🗂";
        string indentationOption = ConfigurationManager.AppSettings["IndentationOption"] ?? "  ";

        return new ConfigTreeParameterizer(folderOption, fileOption, indentationOption);
    }
}