using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Program
{
    /// <summary>
    /// Получает сообщение на выполнение
    /// </summary>
    public static void Main()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Thread.Sleep(80);
                Console.WriteLine($"Получение сообщение {message}");
            };

            channel.BasicConsume(queue: "TaskQueue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}