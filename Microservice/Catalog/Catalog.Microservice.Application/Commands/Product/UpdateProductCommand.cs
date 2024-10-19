using Catalog.Microservice.Application.DTOs.Request;
using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = "";
        public int CatalogId { get; set; }
        public int BrandId { get; set; }
        public List<ProductAttributeRequestDto> ProductAttributes { get; set; } = [];
    }
}
