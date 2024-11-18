using Cart.Microservice.Application.Events;
using Cart.Microservice.Infrastructure.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cart.Microservice.Infrastructure.Messaging
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMQConsumer> _logger;

        public RabbitMQConsumer(
            IModel channel,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMQConsumer> logger)
        {
            _channel = channel;
            _scopeFactory = scopeFactory;
            _logger = logger;

            // Объявляем очередь CartQueue, чтобы быть уверенными, что она существует
            _channel.QueueDeclare(queue: "CartQueue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var wrapper = JsonConvert.DeserializeObject<EventWrapper<object>>(message);
                    _logger.LogInformation($"wrapper: {JsonConvert.SerializeObject(wrapper, Formatting.Indented)}");
                    await HandleEvent(wrapper);

                    _channel.BasicAck(ea.DeliveryTag, multiple: false); // Подтверждаем обработку сообщения
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка обработки сообщения: {Message}", message);
                    // Логируем ошибку, но не подтверждаем сообщение
                }
            };

            _channel.BasicConsume(queue: "CartQueue",
                                  autoAck: false, // Ручное подтверждение
                                  consumer: consumer);
        }

        private async Task HandleEvent(EventWrapper<object> wrapper)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                switch (wrapper.EventType)
                {
                    case nameof(UserDeletedEvent):
                        var deletedEvent = JsonConvert.DeserializeObject<UserDeletedEvent>(wrapper.Data.ToString());
                        if (deletedEvent != null)
                            await mediator.Publish(deletedEvent);
                        break;
                    case nameof(UpdateProductEvent):
                        var updateProductEvent = JsonConvert.DeserializeObject<UpdateProductEvent>(wrapper.Data.ToString());
                        if (updateProductEvent != null)
                            await mediator.Publish(updateProductEvent);
                        break;
                    case nameof(DeleteProductEvent):
                        var deleteEvent = JsonConvert.DeserializeObject<DeleteProductEvent>(wrapper.Data.ToString());
                        if (deleteEvent != null)
                            await mediator.Publish(deleteEvent);
                        break;
                    case nameof(UpdateCatalogEvent):
                        var updateCatalogEvent = JsonConvert.DeserializeObject<UpdateCatalogEvent>(wrapper.Data.ToString());
                        if (updateCatalogEvent != null)
                            await mediator.Publish(updateCatalogEvent);
                        break;
                }
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            base.Dispose();
        }
    }
}
