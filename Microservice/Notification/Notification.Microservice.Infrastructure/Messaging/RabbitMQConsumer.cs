using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Notification.Microservice.Application.Events;
using Notification.Microservice.Infrastructure.Events;
using Notification.Microservice.Infrastructure.Messaging.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Notification.Microservice.Infrastructure.Messaging
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly RabbitMQOptions _options;

        public RabbitMQConsumer(
            IModel channel,
            IOptions<RabbitMQOptions> options,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMQConsumer> logger)
        {
            _channel = channel;
            _scopeFactory = scopeFactory;
            _options = options.Value;
            _logger = logger;

            // Объявляем очередь CartQueue, чтобы быть уверенными, что она существует
            _channel.QueueDeclare(queue: _options.QueueName,
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
                    _logger.LogInformation($"NotificationQueue wrapper: {wrapper}");
                    await HandleEvent(wrapper);

                    _channel.BasicAck(ea.DeliveryTag, multiple: false); // Подтверждаем обработку сообщения
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка обработки сообщения: {Message}", message);
                    // Логируем ошибку, но не подтверждаем сообщение
                }
            };

            _channel.BasicConsume(queue: _options.QueueName,
                                  autoAck: false,
                                  consumer: consumer);
        }

        private async Task HandleEvent(EventWrapper<object> wrapper)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                switch (wrapper.EventType)
                {
                    case nameof(AddNotificationEvent):
                        var addNotificationEvent = JsonConvert.DeserializeObject<AddNotificationEvent>(wrapper.Data.ToString());
                        if (addNotificationEvent != null)
                            await mediator.Publish(addNotificationEvent);
                        break;
                    case nameof(UserDeletedEvent):
                        var userDeletedEvent = JsonConvert.DeserializeObject<UserDeletedEvent>(wrapper.Data.ToString());
                        if (userDeletedEvent != null)
                            await mediator.Publish(userDeletedEvent);
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
