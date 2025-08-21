using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;

public class AddresseeGroup : IAddressee
{
    public ICollection<IAddressee> Addressees { get; } = [];

    internal AddresseeGroup(ICollection<IAddressee> addressees)
    {
        Addressees = addressees;
    }

    public ResultType Receive(IMessage message)
    {
        foreach (IAddressee addressee in Addressees)
        {
            addressee.Receive(message);
        }

        return new ResultType.Success();
    }
}