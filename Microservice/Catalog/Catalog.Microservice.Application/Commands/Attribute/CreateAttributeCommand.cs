using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class CreateAttributeCommand : IRequest<Domain.Entities.Attribute>
    {
        public string Name { get; set; } = "";
        public int AttributeTypeId { get; set; }
    }
}
