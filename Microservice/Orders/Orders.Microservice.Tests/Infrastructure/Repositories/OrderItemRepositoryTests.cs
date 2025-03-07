using Microsoft.EntityFrameworkCore;
using Orders.Microservice.Infrastructure.Persistence;
using Orders.Microservice.Infrastructure.Repositories;
using OrderItemEntity = Orders.Microservice.Domain.Entities.OrderItem;

namespace Orders.Microservice.Tests.Infrastructure.Repositories;

public class OrderItemRepositoryTests
{
    private readonly EFDBContext _context;
    private readonly OrderItemRepository _repository;

    public OrderItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<EFDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new EFDBContext(options);
        _repository = new OrderItemRepository(_context);
    }

    [Fact]
    public async Task GetAllOrderItemsByCatalogId_ShouldReturnCorrectItems()
    {
        const int catalogId = 1;
        var items = new List<OrderItemEntity>
        {
            new OrderItemEntity { Id = Guid.NewGuid(), CatalogId = catalogId },
            new OrderItemEntity { Id = Guid.NewGuid(), CatalogId = 2 }
        };

        await _context.OrderItems.AddRangeAsync(items);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllOrderItemsByCatalogId(catalogId);

        Assert.All(result, i => Assert.Equal(catalogId, i.CatalogId));
    }

    [Fact]
    public async Task GetAllOrderItemsByProductId_ShouldReturnCorrectItems()
    {
        var productId = Guid.NewGuid();
        var items = new List<OrderItemEntity>
        {
            new OrderItemEntity { Id = Guid.NewGuid(), ProductId = productId },
            new OrderItemEntity { Id = Guid.NewGuid(), ProductId = Guid.NewGuid() }
        };

        await _context.OrderItems.AddRangeAsync(items);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllOrderItemsByProductId(productId);

        Assert.Single(result);
        Assert.All(result, i => Assert.Equal(productId, i.ProductId));
    }
}
