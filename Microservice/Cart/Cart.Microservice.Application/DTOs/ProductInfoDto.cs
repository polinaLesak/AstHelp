namespace Cart.Microservice.Application.DTOs
{
    public class ProductInfoDto
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; } = "";
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
    }
}
