using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TourSelector.Data.Models;

namespace TourSelector.Data
{
    public class TourRequestEmitter
    {
        public void Emit(TourRequest tourRequest)
        {
            string requestType = tourRequest.RequestType.ToString().ToLower();
            string exchangeName = "tour_emits";
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, type: "topic", durable: true);

                    var props = channel.CreateBasicProperties();
                    props.DeliveryMode = 2;

                    var routingKey = $"tour.{requestType}";
                    var message = JsonSerializer.Serialize(tourRequest);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchangeName, routingKey, null, body);

                    Console.WriteLine("");
                    Console.WriteLine($"Tour request was emitted using key '{routingKey}'");
                    Console.WriteLine($"This message will be delivered: {message}");
                }
            }
        }
    }
}
