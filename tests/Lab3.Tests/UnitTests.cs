using Itmo.ObjectOrientedProgramming.Lab3.Logger;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity.Factory;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Filtering;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Logging;
using Itmo.ObjectOrientedProgramming.Lab3.Messenger.Message.Entity;
using Moq;
using Xunit;

namespace Lab3.Tests;

public class UnitTests
{
    /// <summary>
    /// 1st test case.
    /// При получении сообщения пользователем, оно сохраняется в статусе “не прочитано”
    /// </summary>
    [Fact]
    public void MessageReceivedByUserShouldBeMarkedUnread()
    {
        // Arrange
        IAddressee userAddressee = new UserAddresseeFactory().Create();
        var topic = new Topic("Hello World", [userAddressee]);

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.High)
            .Build();

        // Act
        topic.Send(message);

        // Assert
        Assert.False(((AddresseeUser)userAddressee).MessageStatuses[message.Id]);
    }

    /// <summary>
    /// 2nd test case.
    /// При попытке отметить сообщение пользователя в статусе “не прочитано”
    /// как прочитанное, оно должно поменять свой статус
    /// </summary>
    [Fact]
    public void MarkUnreadMessageAsReadShouldChangeStatusToRead()
    {
        // Arrange
        IAddressee userAddressee = new UserAddresseeFactory().Create();
        var topic = new Topic("Hello World", [userAddressee]);

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.High)
            .Build();

        // Act
        topic.Send(message);
        ResultType rez = ((AddresseeUser)userAddressee).MarkAsRead(message);

        // Assert
        Assert.True(rez is ResultType.Success);
        Assert.True(((AddresseeUser)userAddressee).MessageStatuses[message.Id]);
    }

    /// <summary>
    /// 3rd test case.
    /// При попытке отметить сообщение пользователя в статусе “прочитано”
    /// как прочитанное, должна вернуться ошибка
    /// </summary>
    [Fact]
    public void MarkReadMessageAsReadShouldReturnFailure()
    {
        // Arrange
        IAddressee userAddressee = new UserAddresseeFactory().Create();
        var topic = new Topic("Hello World", [userAddressee]);

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.High)
            .Build();

        // Act
        topic.Send(message);
        ResultType rez = ((AddresseeUser)userAddressee).MarkAsRead(message);
        ResultType rez2 = ((AddresseeUser)userAddressee).MarkAsRead(message);

        // Assert
        Assert.True(rez is ResultType.Success);
        Assert.True(rez2 is ResultType.Failure);
        Assert.True(((AddresseeUser)userAddressee).MessageStatuses[message.Id]);
    }

    /// <summary>
    /// 4th test case.
    /// При настроенном фильтре для адресата, отправленное сообщение,
    /// не подходящее под критерии важности - до адресата дойти не должно
    /// (в данном тесте необходимо использовать моки)
    /// </summary>
    [Fact]
    public void UserWithFilteringShouldNotReceiveMessagesLessThanImportanceLevel()
    {
        // Arrange
        var mockUserAddressee = new Mock<IAddressee>();
        var filter = new MessageFilterDecorator(mockUserAddressee.Object, ImportanceLevel.High);

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.Low)
            .Build();

        // Act
        ResultType rez = filter.Receive(message);

        // Assert
        Assert.True(rez is ResultType.Failure);
        mockUserAddressee.Verify(a => a.Receive(It.IsAny<IMessage>()), Times.Never);
    }

    /// <summary>
    /// 5th test case.
    /// При настроенном логгировании адресата, должен писаться лог,
    /// когда приходит сообщение
    /// (в данном тесте необходимо использовать моки)
    /// </summary>
    [Fact]
    public void UserWithLoggingShouldLogMessageWhenReceives()
    {
        // Arrange
        var mockUserAddressee = new Mock<IAddressee>();
        mockUserAddressee.Setup(x => x.Receive(It.IsAny<IMessage>())).Returns(new ResultType.Success());

        var mockLogger = new Mock<ILogger>();
        var logger = new LoggingMessagesDecorator(mockUserAddressee.Object, mockLogger.Object);

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.Medium)
            .Build();

        // Act
        ResultType rez = logger.Receive(message);

        // Assert
        Assert.True(rez is ResultType.Success);
        mockLogger.Verify(a => a.Log(It.Is<string>(s => s.Contains("Message received: HI with importance: Medium"))), Times.Once);
    }

    /// <summary>
    /// 6th test case.
    /// При отправке сообщения в месенджер,
    /// его реализация должна производить ожидаемое значение
    /// (в данном тесте необходимо использовать моки)
    /// </summary>
    [Fact]
    public void MessengerShouldShowExpectedOutputWhenReceives()
    {
        // Arrange
        var mockMessenger = new Mock<IMessenger>();
        IAddressee messengerAddressee = new MessengerAddresseeFactory(mockMessenger.Object).Create();

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.Medium)
            .Build();

        // Act
        ResultType rez = messengerAddressee.Receive(message);

        // Assert
        Assert.True(rez is ResultType.Success);
        mockMessenger.Verify(a => a.Print(It.Is<IMessage>(m => m.Header == "HI")), Times.Once);
    }

    /// <summary>
    /// 7th test case.
    /// Добавляются два адресата-пользователя (для одного пользователя),
    /// для одного из них настраивается фильтр важности,
    /// при попытке отправить сообщение с важностью ниже настроенной
    /// – пользователь получает значение единожды.
    /// </summary>
    [Fact]
    public void GroupWithFilteringShouldReceiveMessageOnceWhenFiltered()
    {
        // Arrange
        IAddressee userAddressee = new UserAddresseeFactory().Create();

        var mockUserAddressee = new Mock<IAddressee>();
        var filteredUser = new MessageFilterDecorator(mockUserAddressee.Object, ImportanceLevel.High);

        IAddressee groupAddressee = new GroupAddresseeFactory([userAddressee, filteredUser]).Create();

        SimpleMessage message = SimpleMessage.Builder.WithHeader("HI")
            .WithBody("Message's body")
            .WithImportance(ImportanceLevel.Low)
            .Build();

        // Act
        ResultType rez = groupAddressee.Receive(message);

        // Assert
        Assert.True(rez is ResultType.Success);
        Assert.Contains(message, ((AddresseeUser)userAddressee).Messages);
        mockUserAddressee.Verify(a => a.Receive(It.IsAny<IMessage>()), Times.Never);
    }
}