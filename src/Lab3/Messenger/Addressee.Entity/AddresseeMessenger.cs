using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;

public class AddresseeMessenger : IAddressee
{
    private readonly IMessenger _messenger;

    internal AddresseeMessenger(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public ResultType Receive(IMessage message)
    {
        _messenger.Print(message);

        return new ResultType.Success();
    }
}