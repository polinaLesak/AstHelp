using MediatR;

namespace Catalog.Microservice.Application.Events
{
    public class UpdateProductEvent : INotification
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int CatalogId { get; set; }
        public string CatalogName { get; set; } = "";
        public int Quantity { get; set; }
    }
}
