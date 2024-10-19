using Catalog.Microservice.Domain.Entities;
using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class CreateBrandCommand : IRequest<Brand>
    {
        public string Name { get; set; } = "";
    }
}
