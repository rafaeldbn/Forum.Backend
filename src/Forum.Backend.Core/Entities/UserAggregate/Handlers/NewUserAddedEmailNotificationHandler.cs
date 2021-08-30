using Ardalis.GuardClauses;
using Forum.Backend.Core.Entities.UserAggregate.Events;
using Forum.Backend.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Core.Entities.UserAggregate.Handlers
{
    public class NewUserAddedEmailNotificationHandler : INotificationHandler<NewUserAddedEvent>
    {
        private readonly IEmailSender _emailSender;

        // In a REAL app you might want to use the Outbox pattern and a command/queue here...
        public NewUserAddedEmailNotificationHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // configure a test email server to demo this works
        // https://ardalis.com/configuring-a-local-test-email-server
        public Task Handle(NewUserAddedEvent domainEvent, CancellationToken cancellationToken)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));

            return _emailSender.SendEmailAsync(domainEvent.NewUser.Email, "test@test.com", "User registration", $"{domainEvent.NewUser.Name} your registration has been completed.");
        }
    }
}
