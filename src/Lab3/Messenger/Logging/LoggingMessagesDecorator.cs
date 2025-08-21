using Itmo.ObjectOrientedProgramming.Lab3.Logger;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Logging;

public class LoggingMessagesDecorator : IAddressee
{
    private readonly IAddressee _addressee;
    private readonly ILogger _logger;

    public LoggingMessagesDecorator(IAddressee addressee, ILogger logger)
    {
        _addressee = addressee;
        _logger = logger;
    }

    public ResultType Receive(IMessage message)
    {
        _logger.Log($"Message received: {message.Header} with importance: {message.Importance}");

        return _addressee.Receive(message);
    }
}