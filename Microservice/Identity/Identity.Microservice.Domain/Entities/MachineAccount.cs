namespace Identity.Microservice.Domain.Entities
{
    public class MachineAccount
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
