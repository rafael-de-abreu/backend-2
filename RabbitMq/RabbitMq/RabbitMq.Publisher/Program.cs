using RabbitMq.Shared;
using RabbitMQ.Client;

namespace RabbitMq.Publisher
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("RabbitMq Publisher");

            var factory = new ConnectionFactory();
            using var connection = await factory.CreateConnectionAsync("localhost");
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(RabbitMqConstants.ExchangeName, ExchangeType.Direct);
            await channel.ExchangeDeclareAsync(RabbitMqConstants.TopicExchangeName, ExchangeType.Topic);
            await channel.ExchangeDeclareAsync(RabbitMqConstants.FanoutExchangeName, ExchangeType.Fanout);
            await channel.ExchangeDeclareAsync(RabbitMqConstants.HeadersExchangeName, ExchangeType.Headers);

            await channel.BasicPublishAsync(
                RabbitMqConstants.ExchangeName,
                RabbitMqConstants.RoutingKey,
                body: System.Text.Encoding.UTF8.GetBytes("Hello, RabbitMq!")
            );

            Console.WriteLine("Message published to RabbitMq");

            while (true)
            {
                Console.WriteLine("Enter 1 for direct, 2 for Topic, 3 for Fanout or 4 for Header");
                var exchangeType = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Enter the message:");
                var newMessage = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(newMessage))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                if (exchangeType == "1")
                {
                    await channel.BasicPublishAsync(
                        RabbitMqConstants.ExchangeName,
                        RabbitMqConstants.RoutingKey,
                        body: System.Text.Encoding.UTF8.GetBytes(newMessage)
                    );
                    Console.WriteLine("Message published to direct exchange");
                }
                else if (exchangeType == "2")
                {
                    Console.WriteLine("Enter the routing key (1 - Info, 2 - Warning, 3 - Error):");
                    var routingKey = Console.ReadLine() ?? string.Empty;

                    if (routingKey == "1")
                    {
                        routingKey = RabbitMqConstants.TopicRoutingKeyInfo;
                    }
                    else if (routingKey == "2")
                    {
                        routingKey = RabbitMqConstants.TopicRoutingKeyWarning;
                    }
                    else if (routingKey == "3")
                    {
                        routingKey = RabbitMqConstants.TopicRoutingKeyError;
                    }
                    else
                    {
                        Console.WriteLine("Invalid routing key. Please enter 1, 2, or 3.");
                        continue;
                    }

                    await channel.BasicPublishAsync(
                        RabbitMqConstants.TopicExchangeName,
                        routingKey,
                        body: System.Text.Encoding.UTF8.GetBytes(newMessage)
                    );
                }
                else if (exchangeType == "3")
                {
                    await channel.BasicPublishAsync(
                         RabbitMqConstants.FanoutExchangeName,
                         routingKey: string.Empty,
                         body: System.Text.Encoding.UTF8.GetBytes(newMessage)
                     );
                }
                else if (exchangeType == "4")
                {
                    var headers = new Dictionary<string, object?>()
                    {
                        {RabbitMqConstants.HeadersKeyName, RabbitMqConstants.HeadersKeyValue }
                    };
                    var properties = new BasicProperties
                    {
                        Headers = headers
                    };
                    await channel.BasicPublishAsync(
                        RabbitMqConstants.HeadersExchangeName,
                        routingKey: string.Empty,
                        mandatory: false,
                        basicProperties: properties,
                        body: System.Text.Encoding.UTF8.GetBytes(newMessage)
                    );
                }
                else
                {
                    Console.WriteLine("Please enter a valid number (1 or 2)");
                    continue;
                }
            }
        }
    }
}