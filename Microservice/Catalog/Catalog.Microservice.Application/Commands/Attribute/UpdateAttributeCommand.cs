using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class UpdateAttributeCommand : IRequest<Domain.Entities.Attribute>
    {
        public int AttributeId { get; set; }
        public string Name { get; set; } = "";
        public int AttributeTypeId { get; set; }
    }
}
