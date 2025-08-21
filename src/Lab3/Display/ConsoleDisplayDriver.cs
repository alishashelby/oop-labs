using Crayon;
using System;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Display;

public class ConsoleDisplayDriver : IDisplayDriver
{
    public Color TextColor { get; set; } = Color.White;

    public void Clear() => Console.Clear();

    public void Write(string text)
    {
        Console.WriteLine(Output.Rgb(TextColor.R, TextColor.G, TextColor.B).Text(text));
    }
}