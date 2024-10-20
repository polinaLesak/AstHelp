namespace Cart.Microservice.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; } // ID пользователя, которому принадлежит корзина
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
