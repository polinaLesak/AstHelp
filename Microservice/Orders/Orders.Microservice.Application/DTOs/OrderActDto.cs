using Orders.Microservice.Domain.Entities;

namespace Orders.Microservice.Application.DTOs
{
    public class OrderActDto
    {
        public Order Order { get; set; }
        public string CustomerFullname { get; set; }
        public string AdminFullname { get; set; }
        public string CustomerPosition { get; set; }
        public string AdminPosition { get; set; }
    }
}
