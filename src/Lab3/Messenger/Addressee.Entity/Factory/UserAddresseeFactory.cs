namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity.Factory;

public class UserAddresseeFactory : IAddresseeFactory
{
    public IAddressee Create()
    {
        return new AddresseeUser();
    }
}