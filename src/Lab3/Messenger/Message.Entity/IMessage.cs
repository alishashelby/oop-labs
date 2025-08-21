using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

public enum ImportanceLevel
{
    Low,
    Medium,
    High,
}

public interface IMessage
{
    Guid Id { get; }

    string Header { get; }

    string Body { get; }

    ImportanceLevel Importance { get; }
}