using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Entities.UserAggregate.Events;
using Forum.Backend.Core.Entities.UserAggregate.Handlers;
using Forum.Backend.Core.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.UnitTests.Core.Handlers
{
    public class NewUserAddedEmailNotificationHandlerHandle
    {
        private NewUserAddedEmailNotificationHandler _handler;
        private Mock<IEmailSender> _emailSenderMock;

        public NewUserAddedEmailNotificationHandlerHandle()
        {
            _emailSenderMock = new Mock<IEmailSender>();
            _handler = new NewUserAddedEmailNotificationHandler(_emailSenderMock.Object);
        }

        [Fact]
        public async Task ThrowsExceptionGivenNullEventArgument()
        {
            Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task SendsEmailGivenEventInstance()
        {
            await _handler.Handle(new NewUserAddedEvent(new User("Test", "Test@test.com", "123456", "TimeZone")), CancellationToken.None);

            _emailSenderMock.Verify(sender => sender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
