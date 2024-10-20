using MediatR;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class GetOrdersByManagerIdQueryHandler : IRequestHandler<GetOrdersByManagerIdQuery, IEnumerable<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByManagerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByManagerIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Orders.GetOrdersByManagerIdAsync(request.ManagerId);
        }
    }
}
