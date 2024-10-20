namespace Cart.Microservice.Application.DTOs.Command
{
    public class AddToCartCommandDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
