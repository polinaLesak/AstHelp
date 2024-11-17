namespace Orders.Microservice.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFullname { get; set; } = "";
        public string CustomerPosition { get; set; } = "";
        public int ManagerId { get; set; }
        public string ManagerFullname { get; set; } = "";
        public string ManagerPosition { get; set; } = "";
        public string ReasonForIssue { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
