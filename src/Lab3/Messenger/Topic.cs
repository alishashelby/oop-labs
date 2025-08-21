using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;
using System.Collections.Generic;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger;

public class Topic
{
    public string TopicName { get; }

    public ICollection<IAddressee> Addressees { get; }

    public Topic(string topicName, ICollection<IAddressee> recipients)
    {
        TopicName = topicName;
        Addressees = recipients;
    }

    public void Send(IMessage message)
    {
        foreach (IAddressee addressee in Addressees)
        {
            addressee.Receive(message);
        }
    }
}