using Crayon;
using System;
using System.Drawing;
using System.IO;

namespace Itmo.ObjectOrientedProgramming.Lab3.Display.FileDisplayDriver;

public class TxtFileDisplayDriver : IDisplayDriver
{
    private readonly string _filePath;

    public Color TextColor { get; set; } = Color.White;

    public TxtFileDisplayDriver(string filePath)
    {
        _filePath = filePath;
    }

    public void Clear()
    {
        File.WriteAllText(_filePath, string.Empty);
    }

    public void Write(string text)
    {
        File.AppendAllText(_filePath, Output.Rgb(TextColor.R, TextColor.G, TextColor.B).Text(text) + Environment.NewLine);
    }
}