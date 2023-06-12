using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "PilaPrincipal", 
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

channel.QueueDeclare(queue: "Fallidas", 
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

var consumer = new EventingBasicConsumer(channel);
//consumer.Received += (model, eventArgs) => {
//    var body = eventArgs.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    //Console.WriteLine($"Post message received: {message}");
//    InsertToBD(message);
//};

//channel.BasicConsume(queue: "PilaPrincipal", autoAck: true, consumer: consumer);

consumer.Received += (model, ea) =>
{
    try
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        //Console.WriteLine("Mensaje recibido: {0}", message);
        var properties = ea.BasicProperties;
        if (properties.Headers != null && properties.Headers.ContainsKey("messageType"))
        {
            var messageType = Encoding.UTF8.GetString((byte[])properties.Headers["messageType"]);
            Console.WriteLine("Tipo de mensaje: " + messageType.ToUpper());
        }
        InsertToBD(message);
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        Thread.Sleep(5000);
    }
    catch (Exception e)
    {
        channel.BasicPublish(exchange: "",
                             routingKey: "Fallidas",
                             basicProperties: null,
                             body: ea.Body);

        Console.WriteLine("Error al procesar el mensaje: {0}", e.Message);
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
};
channel.BasicConsume(queue: "PilaPrincipal",
                                 autoAck: false,
                                 consumer: consumer);

Console.ReadKey();

void InsertToBD(string message) {
    Console.WriteLine($"Insertando mensaje en BD: {message}");
}