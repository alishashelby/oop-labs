using Itmo.ObjectOrientedProgramming.Lab3.Display;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;
using System.Drawing;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;

public class AddresseeDisplay : IAddressee
{
    private readonly IDisplayDriver _driver;

    internal AddresseeDisplay(IDisplayDriver driver)
    {
        _driver = driver;
    }

    public void SetColor(Color color)
    {
        _driver.TextColor = color;
    }

    public ResultType Receive(IMessage message)
    {
        _driver.Clear();
        _driver.Write($"{message.Header}: {message.Body}");

        return new ResultType.Success();
    }
}