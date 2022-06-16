using System;
using RabbitMQ.Client;
using System.Text;

/// <summary>
/// Отправка простого сообщения в очередь hello
/// </summary>
public class Program
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "Hello",
                                 durable: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = "Started RabbitMq";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($"Отправлено сообщение: {message}");
        }
        Console.ReadLine();
    }
}