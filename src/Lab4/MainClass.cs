using Itmo.ObjectOrientedProgramming.Lab4.Stream;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public static class MainClass
{
    public static void Main(string[] arg)
    {
        var stream = new ConsoleStream();
        var app = new FileSystemApp(stream);
        app.Run();
    }
}