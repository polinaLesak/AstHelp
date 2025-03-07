using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Infrastructure.Persistence;
using Orders.Microservice.Infrastructure.Repositories;
using OrderEntity = Orders.Microservice.Domain.Entities.Order;
using OrderItemEntity = Orders.Microservice.Domain.Entities.OrderItem;

namespace Orders.Microservice.Tests.Infrastructure.Repositories;

public class OrderRepositoryTests
{
    private readonly EFDBContext _context;
    private readonly OrderRepository _repository;

    public OrderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EFDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new EFDBContext(options);
        _repository = new OrderRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllOrdersWithItems()
    {
        var orders = new List<OrderEntity>
        {
            new OrderEntity { Id = Guid.NewGuid(), CustomerId = 1, Items = new List<OrderItemEntity> 
                { new OrderItemEntity { Id = Guid.NewGuid(), ProductId = Guid.NewGuid() } } },
            new OrderEntity { Id = Guid.NewGuid(), CustomerId = 2 }
        };

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, o => o.Items.Count > 0);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOrderWithItems()
    {
        var orderId = Guid.NewGuid();
        var order = new OrderEntity
        {
            Id = orderId,
            CustomerId = 1,
            Items = new List<OrderItemEntity>
            {
                new OrderItemEntity { Id = Guid.NewGuid(), ProductId = Guid.NewGuid() }
            }
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(orderId);

        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
        Assert.Single(result.Items);
    }

    [Fact]
    public async Task CountByManagerIdAsync_ShouldReturnCorrectCount()
    {
        var orders = new List<OrderEntity>
        {
            new OrderEntity { Id = Guid.NewGuid(), ManagerId = 1 },
            new OrderEntity { Id = Guid.NewGuid(), ManagerId = 1 },
            new OrderEntity { Id = Guid.NewGuid(), ManagerId = 2 }
        };

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();

        var count = await _repository.CountByManagerIdAsync(1);

        Assert.Equal(2, count);
    }

    [Fact]
    public async Task GetOrdersByCustomerIdAsync_ShouldReturnCustomerOrders()
    {
        var customerId = 1;
        var orders = new List<OrderEntity>
        {
            new OrderEntity { Id = Guid.NewGuid(), CustomerId = customerId },
            new OrderEntity { Id = Guid.NewGuid(), CustomerId = 2 }
        };

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();

        var result = await _repository.GetOrdersByCustomerIdAsync(customerId);

        Assert.Single(result);
        Assert.All(result, o => Assert.Equal(customerId, o.CustomerId));
    }
}
