using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity.Factory;

public class GroupAddresseeFactory : IAddresseeFactory
{
    private readonly ICollection<IAddressee> _addressees;

    public GroupAddresseeFactory(ICollection<IAddressee> addressees)
    {
        _addressees = addressees;
    }

    public IAddressee Create()
    {
        return new AddresseeGroup(_addressees);
    }
}