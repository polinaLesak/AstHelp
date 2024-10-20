using MediatR;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Application.Queries;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);

            if (order == null)
                throw new NotFoundException($"Заявка с ID \"{request.OrderId}\" не найдена.");

            return order;
        }
    }
}
