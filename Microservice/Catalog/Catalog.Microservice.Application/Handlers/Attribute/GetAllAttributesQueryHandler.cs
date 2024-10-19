using Catalog.Microservice.Application.Queries;
using Catalog.Microservice.Domain.Repositories;
using MediatR;

namespace Catalog.Microservice.Application.Handlers
{
    public class GetAllAttributesQueryHandler : IRequestHandler<GetAllAttributesQuery, IEnumerable<Domain.Entities.Attribute>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAttributesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Entities.Attribute>> Handle(GetAllAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Attributes.GetAllAsync();
        }
    }
}
