using Forum.Backend.SharedKernel;

namespace Forum.Backend.Core.Entities.UserAggregate.Events
{
    public class NewUserAddedEvent : BaseDomainEvent
    {
        public User NewUser { get; set; }

        public NewUserAddedEvent(User user)
        {
            NewUser = user;
        }
    }
}
