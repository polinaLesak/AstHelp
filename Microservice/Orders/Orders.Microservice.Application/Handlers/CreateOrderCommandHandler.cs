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
        private readonly IdentityService _identityService;
        private readonly CatalogService _catalogService;
        private readonly CartService _cartService;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IdentityService identityService,
            CatalogService catalogService,
            CartService cartService)
        {
            _unitOfWork = unitOfWork;
            _identityService = identityService;
            _catalogService = catalogService;
            _cartService = cartService;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var managers = await _identityService.GetAllManagersAsync();
            var managerWithLeastOrders = await GetManagerWithLeastOrdersAsync(managers);
            var customerInfo = await _identityService.GetUserInfoById(request.CustomerId);

            var productsInfo = await _catalogService.GetProductsInfoAsync(request.Items.Select(x => x.ProductId).ToArray());

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                CustomerFullname = customerInfo.Profile.Fullname,
                CustomerPosition = customerInfo.Profile.Position,
                ManagerId = managerWithLeastOrders.Id,
                ManagerFullname = managerWithLeastOrders.Profile.Fullname,
                ManagerPosition = managerWithLeastOrders.Profile.Position,
                ReasonForIssue = request.ReasonForIssue,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = request.Items.Select(i =>
                {
                    var productInfo = productsInfo.FirstOrDefault(x => x.ProductId == i.ProductId);
                    return new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        CatalogId = productInfo.CatalogId,
                        CatalogName = productInfo.CatalogName,
                        ProductId = productInfo.ProductId,
                        ProductName = productInfo.ProductName,
                        Quantity = i.Quantity
                    };
                }).ToList()
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _cartService.ResetCartByUserId(request.CustomerId);
            await _unitOfWork.CommitAsync();

            return order;
        }

        private async Task<UserInfo> GetManagerWithLeastOrdersAsync(List<UserInfo> managers)
        {
            var managerOrderCounts = new Dictionary<int, int>();

            foreach (var manager in managers)
            {
                var orderCount = await _unitOfWork.Orders.CountByManagerIdAsync(manager.Id);
                managerOrderCounts[manager.Id] = orderCount;
            }

            var managerIdWithLeastOrders = managerOrderCounts.OrderBy(kvp => kvp.Value).First().Key;
            return managers.First(m => m.Id == managerIdWithLeastOrders);
        }
    }
}
