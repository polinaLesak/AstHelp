using MediatR;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.Excel;
using Orders.Microservice.Application.Exceptions;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class GenerateOrderReportCommandHandler : IRequestHandler<GenerateOrderReportCommand, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateOrderReportCommandHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(GenerateOrderReportCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException($"Заказ с ID {request.OrderId} не найден.");

            return await OrderReportExcel.ProcessExcel(order);
        }
    }
}
