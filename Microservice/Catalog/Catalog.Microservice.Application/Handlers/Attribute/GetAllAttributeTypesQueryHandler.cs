using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAllAttributeTypesQueryHandler
        : IRequestHandler<GetAllAttributeTypesQuery, IEnumerable<Domain.Entities.AttributeType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAttributeTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.AttributeType>> Handle(
            GetAllAttributeTypesQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.AttributeTypes.GetAllAsync();
        }
    }
}
