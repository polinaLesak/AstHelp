using MediatR;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Заявка с ID \"{request.OrderId}\" не найдена.");

            order.Status = request.NewStatus;

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
