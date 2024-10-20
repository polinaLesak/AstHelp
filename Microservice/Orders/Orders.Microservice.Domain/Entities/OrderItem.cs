using System.Text.Json.Serialization;

namespace Orders.Microservice.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }

        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
