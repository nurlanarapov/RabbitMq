using System;
using RabbitMQ.Client;
using System.Text;

public class Program
{
    /// <summary>
    /// Посылает сообщение работникам на выполнение и автоматически удаляется
    /// </summary>
    public static void Main()
    {
        int i = 1;
        do
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var message = $"Task {i}";
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();

                channel.BasicPublish(exchange: "",
                                     routingKey: "TaskQueue",
                                     basicProperties: properties,
                                     body: body);

                Console.WriteLine($"Отправлено сообщение: {message}");
            }
            i++;
            Thread.Sleep(60);
        }
        while (true);
        
    }
}