using MediatR;
using Orders.Microservice.Application.Commands;
using Orders.Microservice.Application.DTOs;
using Orders.Microservice.Application.Service;
using Orders.Microservice.Domain.Entities;
using Orders.Microservice.Domain.Repositories;

namespace Orders.Microservice.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Получаем всех менеджеров
            var managers = await _identityService.GetAllManagersAsync();

            // Подсчитываем количество заказов каждого менеджера
            var managerWithLeastOrders = await GetManagerWithLeastOrdersAsync(managers);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                CustomerFullname = request.CustomerFullname,
                CustomerPosition = request.CustomerPosition,
                ManagerId = managerWithLeastOrders.Id,
                ManagerFullname = managerWithLeastOrders.Profile.Fullname,
                ManagerPosition = managerWithLeastOrders.Profile.Position,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = request.Items.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductName = i.ProductName,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return order;
        }

        private async Task<UserDto> GetManagerWithLeastOrdersAsync(List<UserDto> managers)
        {
            var managerOrderCounts = new Dictionary<int, int>();

            foreach (var manager in managers)
            {
                var orderCount = await _unitOfWork.Orders.CountByManagerIdAsync(manager.Id);
                managerOrderCounts[manager.Id] = orderCount;
            }

            // Находим менеджера с наименьшим количеством заказов
            var managerIdWithLeastOrders = managerOrderCounts.OrderBy(kvp => kvp.Value).First().Key;
            return managers.First(m => m.Id == managerIdWithLeastOrders);
        }
    }
}
