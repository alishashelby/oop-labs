using System;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;

public class SimpleMessage : IMessage
{
    public Guid Id { get; }

    public string Header { get; }

    public string Body { get; }

    public ImportanceLevel Importance { get; }

    private SimpleMessage(string header, string body, ImportanceLevel importance)
    {
        Header = header;
        Body = body;
        Importance = importance;
        Id = Guid.NewGuid();
    }

    public static MessageBuilder Builder => new();

    public class MessageBuilder
    {
        private string? _header;
        private string? _body;
        private ImportanceLevel? _importance;

        public MessageBuilder WithHeader(string header)
        {
            _header = header;
            return this;
        }

        public MessageBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public MessageBuilder WithImportance(ImportanceLevel importance)
        {
            _importance = importance;
            return this;
        }

        public SimpleMessage Build()
        {
            return new SimpleMessage(
                _header ?? throw new InvalidOperationException("Header should be provided"),
                _body ?? throw new InvalidOperationException("Body should be provided"),
                _importance ?? throw new InvalidOperationException("Importance should be provided"));
        }
    }
}