using MediatR;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<Order>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Orders.GetOrdersByCustomerIdAsync(request.CustomerId);
        }
    }
}
