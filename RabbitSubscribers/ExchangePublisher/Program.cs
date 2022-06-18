using System;
using System.Text;
using RabbitMQ.Client;

public class Program
{
    public static void Main()
    {
        int i = 0;
        do
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("Logs", type: ExchangeType.Fanout);

                var message = $"Message from exchange {i}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "Logs", routingKey: "", basicProperties: null
                                     , body: body);

                Console.WriteLine($"Sended message: {message}");
                Thread.Sleep(1000);
            }
            i++;
        }
        while (true);      
    }
}