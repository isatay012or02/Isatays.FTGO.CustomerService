using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Isatays.FTGO.CustomerService.Infrastructure.Services;

public class RabbitMqService
{
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { Uri = new Uri("amqps://ocwiksvx:wcGxIMwWx6oU4m50hfmdns2JASmpQXsj@mustang.rmq.cloudamqp.com/ocwiksvx") };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "MyQueue",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                           routingKey: "MyQueue",
                           basicProperties: null,
                           body: body);
        }
    }
}
