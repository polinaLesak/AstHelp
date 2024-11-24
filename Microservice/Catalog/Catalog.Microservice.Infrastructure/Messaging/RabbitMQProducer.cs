using Catalog.Microservice.Infrastructure.Events;
using Catalog.Microservice.Infrastructure.Messaging.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Catalog.Microservice.Infrastructure.Messaging
{
    public class RabbitMQProducer
    {
        private readonly IModel _channel;
        private readonly RabbitMQOptions _options;
        private readonly ILogger<RabbitMQProducer> _logger;

        public RabbitMQProducer(
            IModel channel,
            IOptions<RabbitMQOptions> options,
            ILogger<RabbitMQProducer> logger)
        {
            _channel = channel;
            _options = options.Value;
            _logger = logger;
        }

        public void Publish<T>(T @event)
        {
            try
            {
                var eventType = @event.GetType().Name;
                var eventWrapper = new EventWrapper<T>(eventType, @event);

                var message = JsonConvert.SerializeObject(eventWrapper);
                _logger.LogInformation($"wrapper: {message}");
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(exchange: _options.ExchangeName,
                                      routingKey: _options.QueueName,
                                      basicProperties: null,
                                      body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error serializing message: {ex.Message}");
            }
        }
    }
}
