using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Orders.Microservice.Infrastructure.Events;
using Orders.Microservice.Infrastructure.Messaging.Configurations;
using RabbitMQ.Client;
using System.Text;

namespace Orders.Microservice.Infrastructure.Messaging
{
    public class RabbitMQProducer
    {
        private readonly IModel _channel;
        private readonly RabbitMQOptions _options;

        public RabbitMQProducer(IModel channel, IOptions<RabbitMQOptions> options)
        {
            _channel = channel;
            _options = options.Value;
        }

        public void Publish<T>(T @event)
        {
            var eventType = @event.GetType().Name;
            var eventWrapper = new EventWrapper<T>(eventType, @event);

            var message = JsonConvert.SerializeObject(eventWrapper);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _options.ExchangeName,
                                  routingKey: _options.QueueName,
                                  basicProperties: null,
                                  body: body);
        }
    }
}
