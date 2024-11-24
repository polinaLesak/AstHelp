using Catalog.Microservice.Application.Commands;
using Catalog.Microservice.Application.Exceptions;
using Catalog.Microservice.Application.Service;
using Catalog.Microservice.Domain.Repositories;
using Catalog.Microservice.Infrastructure.Messaging;
using MediatR;
using Orders.Microservice.Application.Events;

namespace Catalog.Microservice.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly RabbitMQProducer _rabbitMQProducer;

        public DeleteProductCommandHandler(
            IUnitOfWork unitOfWork,
            IFileService fileService,
            RabbitMQProducer rabbitMQProducer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new NotFoundException($"Продукт с ID \"{request.ProductId}\" не найден.");
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                _fileService.DeleteFile(product.ImageUrl);
            }

            _unitOfWork.Products.Remove(product);

            _rabbitMQProducer.Publish(new DeleteProductEvent
            {
                ProductId = product.Id
            });

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
