using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

/// <summary>
/// Получение простого сообщения из очереди hello
/// </summary>
public class Program
{
    public static void Main()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Получение сообщение {message}");
            };

            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}