namespace Orders.Microservice.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
    }
}
