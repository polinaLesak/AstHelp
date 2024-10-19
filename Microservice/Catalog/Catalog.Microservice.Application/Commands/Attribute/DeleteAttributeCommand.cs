using MediatR;

namespace Catalog.Microservice.Application.Commands
{
    public class DeleteAttributeCommand : IRequest<Unit>
    {
        public int AttributeId { get; }

        public DeleteAttributeCommand(int attributeId)
        {
            AttributeId = attributeId;
        }
    }
}
