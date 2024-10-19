using Catalog.Microservice.Application.DTOs.Request;
using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = "";
        public int CatalogId { get; set; }
        public int BrandId { get; set; }
        public List<ProductAttributeRequestDto> ProductAttributes { get; set; } = [];
    }
}
