using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;

public interface IAddressee
{
    ResultType Receive(IMessage message);
}