using Identity.Microservice.Domain.Entities;

namespace Identity.Microservice.Domain.Events
{
    public class UserUpdatedEvent
    {
        public User User { get; set; }

        public UserUpdatedEvent(User user)
        {
            User = user;
        }
    }
}
