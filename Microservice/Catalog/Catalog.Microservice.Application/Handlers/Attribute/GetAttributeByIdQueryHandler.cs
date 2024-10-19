using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAttributeByIdQueryHandler : IRequestHandler<GetAttributeByIdQuery, Domain.Entities.Attribute>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAttributeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Attribute> Handle(GetAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Attributes.GetByIdAsync(request.AttributeId);
        }
    }
}
