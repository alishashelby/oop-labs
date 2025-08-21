using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Filtering;

public class MessageFilterDecorator : IAddressee
{
    private readonly IAddressee _addressee;

    private readonly ImportanceLevel _importanceLevel;

    public MessageFilterDecorator(IAddressee addressee, ImportanceLevel importanceLevel)
    {
        _addressee = addressee;
        _importanceLevel = importanceLevel;
    }

    public ResultType Receive(IMessage message)
    {
        if (message.Importance >= _importanceLevel)
        {
            _addressee.Receive(message);

            return new ResultType.Success();
        }

        return new ResultType.Failure();
    }
}