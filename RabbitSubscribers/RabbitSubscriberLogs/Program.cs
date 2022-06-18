using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Program
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare("Logs", type: ExchangeType.Fanout);

            var QueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: QueName, exchange: "Logs",
                             routingKey: "");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                
            };

            channel.BasicConsume(queue: QueName, autoAck: true, consumer: consumer);
            Console.ReadLine();
        }
        
    }
}