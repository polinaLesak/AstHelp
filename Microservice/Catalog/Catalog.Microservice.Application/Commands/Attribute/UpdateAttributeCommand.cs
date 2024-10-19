using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateAttributeCommand : IRequest<Domain.Entities.Attribute>
    {
        public int AttributeId { get; }
        public string Name { get; } = "";
        public int AttributeTypeId { get; }
    }
}
