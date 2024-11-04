namespace Notification.Microservice.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = "";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
