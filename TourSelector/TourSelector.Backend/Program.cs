using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("BACKEND");
Console.WriteLine("");

string exchangeName = "tour_emits";
string bindingKey = $"tour.*";
var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(exchangeName, type: "topic", durable: true);
        var queueName = channel.QueueDeclare(durable: true).QueueName;

        channel.QueueBind(queueName, exchangeName, bindingKey);

        Console.WriteLine($"Backend is waiting for messages on queue '{queueName}'...");
        Console.WriteLine("");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;

            Console.WriteLine($"Backend received message: {message}");
            Console.WriteLine($"by the use of routing key '{routingKey}'");
            Console.WriteLine("");
        };

        channel.BasicConsume(queueName, autoAck: true, consumer);

        Console.ReadLine();
    }
}