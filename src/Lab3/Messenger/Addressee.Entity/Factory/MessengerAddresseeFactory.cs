namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity.Factory;

public class MessengerAddresseeFactory : IAddresseeFactory
{
    private readonly IMessenger _messenger;

    public MessengerAddresseeFactory(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public IAddressee Create()
    {
        return new AddresseeMessenger(_messenger);
    }
}