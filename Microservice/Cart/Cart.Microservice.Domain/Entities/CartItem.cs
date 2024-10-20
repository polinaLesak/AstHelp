namespace Cart.Microservice.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; } // ID корзины
        public Guid ProductId { get; set; } // ID товара
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
    }
}
