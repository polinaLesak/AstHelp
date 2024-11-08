using System.Text.Json.Serialization;

namespace Cart.Microservice.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public int CatalogId { get; set; }
        public string CatalogName { get; set; } = "";
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
