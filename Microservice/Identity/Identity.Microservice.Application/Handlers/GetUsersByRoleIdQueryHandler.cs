using Identity.Microservice.Application.Queries;
using Identity.Microservice.Domain.Entities;
using Identity.Microservice.Domain.Repositories;
using MediatR;

namespace Identity.Microservice.Application.Handlers
{
    public class GetUsersByRoleIdQueryHandler : IRequestHandler<GetUsersByRoleIdQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersByRoleIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersByRoleIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users.GetUsersByRoleIdAsync(request.RoleId);
        }
    }
}
