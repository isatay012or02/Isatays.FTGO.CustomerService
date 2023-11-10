using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace Isatays.FTGO.CustomerService.Infrastructure.Services;

public class RabbitMqListener : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqListener()
    {
        var factory = new ConnectionFactory { Uri = new Uri("amqps://ocwiksvx:wcGxIMwWx6oU4m50hfmdns2JASmpQXsj@mustang.rmq.cloudamqp.com/ocwiksvx") };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var str = ea.Exchange;
            //Debug.WriteLine($"Получено сообщение: {content}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("MyQueue", false, consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
