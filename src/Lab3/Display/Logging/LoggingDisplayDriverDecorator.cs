using Itmo.ObjectOrientedProgramming.Lab3.Logger;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Display.Logging;

public class LoggingDisplayDriverDecorator : IDisplayDriver
{
    private readonly IDisplayDriver _displayDriver;
    private readonly ILogger _logger;

    public LoggingDisplayDriverDecorator(IDisplayDriver displayDriver, ILogger logger)
    {
        _displayDriver = displayDriver;
        _logger = logger;
    }

    public Color TextColor
    {
        get => _displayDriver.TextColor;
        set
        {
            _logger.Log($"Color of the text is set to RGB({value.R}, {value.G}, {value.B})");
            _displayDriver.TextColor = value;
        }
    }

    public void Clear()
    {
        _logger.Log("Display cleared.");
        _displayDriver.Clear();
    }

    public void Write(string text)
    {
        _logger.Log($"Display writing: {text}");
        _displayDriver.Write(text);
    }
}