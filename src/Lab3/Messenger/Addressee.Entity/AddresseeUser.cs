using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;
using System;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;

public class AddresseeUser : IAddressee
{
    public ICollection<IMessage> Messages { get; } = [];

    public Dictionary<Guid, bool> MessageStatuses { get; } = [];

    public ResultType Receive(IMessage message)
    {
        Messages.Add(message);
        MessageStatuses[message.Id] = false;

        return new ResultType.Success();
    }

    public ResultType MarkAsRead(IMessage message)
    {
        if (MessageStatuses.ContainsKey(message.Id) && !MessageStatuses[message.Id])
        {
            MessageStatuses[message.Id] = true;

            return new ResultType.Success();
        }

        return new ResultType.Failure();
    }
}