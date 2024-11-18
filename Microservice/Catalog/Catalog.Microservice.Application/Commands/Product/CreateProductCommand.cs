using Catalog.Microservice.Application.DTOs.Request;
using Catalog.Microservice.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Catalog.Microservice.Application.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = "";
        public int CatalogId { get; set; }
        public int BrandId { get; set; }
        public int Quantity { get; set; }
        public IFormFile? Image { get; set; }
        public List<ProductAttributeRequestDto> ProductAttributes { get; set; } = [];
    }
}
