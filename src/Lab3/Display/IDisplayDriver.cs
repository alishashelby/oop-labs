using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Display;

public interface IDisplayDriver
{
    Color TextColor { get; set; }

    void Clear();

    void Write(string text);
}