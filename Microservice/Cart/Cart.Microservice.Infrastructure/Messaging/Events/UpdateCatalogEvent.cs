using MediatR;

namespace Cart.Microservice.Application.Events
{
    public class UpdateCatalogEvent : INotification
    {
        public int CatalogId { get; set; }
        public string CatalogName { get; set; } = "";
    }
}
