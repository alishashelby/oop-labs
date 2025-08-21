using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger;

public interface IMessenger
{
    void Print(IMessage message);
}