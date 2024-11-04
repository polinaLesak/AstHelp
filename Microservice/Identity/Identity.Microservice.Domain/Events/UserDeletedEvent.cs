namespace Identity.Microservice.Domain.Events
{
    public class UserDeletedEvent
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserDeletedEvent(int userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}
