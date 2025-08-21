using System;

namespace Itmo.ObjectOrientedProgramming.Lab4.Stream;

public class ConsoleStream : IStream
{
    public string GetInput()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public void Write(string output)
    {
        Console.WriteLine(output);
    }
}