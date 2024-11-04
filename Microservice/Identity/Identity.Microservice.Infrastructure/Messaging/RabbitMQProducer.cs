using Identity.Microservice.Domain.Events;
using Identity.Microservice.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Identity.Microservice.Infrastructure.Messaging
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

            _channel.BasicPublish(exchange: _options.IdentityExchangeName,
                                  routingKey: _options.QueueName,
                                  basicProperties: null,
                                  body: body);
        }
    }
}
