using MediatR;

namespace Catalog.Microservice.Application.Queries
{
    public class GetAttributeByIdQuery : IRequest<Domain.Entities.Attribute>
    {
        public int AttributeId { get; set; }

        public GetAttributeByIdQuery(int attributeId)
        {
            AttributeId = attributeId;
        }
    }
}
