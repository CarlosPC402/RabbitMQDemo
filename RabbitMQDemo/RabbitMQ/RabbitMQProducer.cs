using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQDemo.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendMessage<T>(T message, string messageType)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            
            var connection = factory.CreateConnection();
            
            using var channel = connection.CreateModel();

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>();
            properties.Headers.Add("messageType", messageType);

            channel.QueueDeclare(queue: "PilaPrincipal", 
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
            
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            
            channel.BasicPublish(exchange: "", routingKey: "PilaPrincipal", basicProperties: properties, body: body);
        }
    }
}
