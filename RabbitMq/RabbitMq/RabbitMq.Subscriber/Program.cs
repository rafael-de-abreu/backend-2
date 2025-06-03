using RabbitMq.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMq.Subscriber
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("RabbitMq Subscriber");

            var factory = new ConnectionFactory();
            using var connection = await factory.CreateConnectionAsync("localhost");
            using var channel = await connection.CreateChannelAsync();

            // Declarar o exchange e a fila para o tipo Direct
            await channel.ExchangeDeclareAsync(RabbitMqConstants.ExchangeName, ExchangeType.Direct);
            await channel.QueueDeclareAsync(RabbitMqConstants.QueueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(
                RabbitMqConstants.QueueName,
                RabbitMqConstants.ExchangeName,
                RabbitMqConstants.RoutingKey
            );

            // Declarar o exchange e a fila para o tipo Topic
            await channel.ExchangeDeclareAsync(RabbitMqConstants.TopicExchangeName, ExchangeType.Topic);
            await channel.QueueDeclareAsync(RabbitMqConstants.TopicQueueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(
                RabbitMqConstants.TopicQueueName,
                RabbitMqConstants.TopicExchangeName,
                args.Length > 0 ? args[0] : "Messages.*" 
            );

            // Declarar o exchange e a fila para o tipo Fanout
            await channel.ExchangeDeclareAsync(RabbitMqConstants.FanoutExchangeName, ExchangeType.Fanout);
            var fanoutQueue = await channel.QueueDeclareAsync();
            await channel.QueueBindAsync(
                fanoutQueue.QueueName,
                RabbitMqConstants.FanoutExchangeName,
                string.Empty
            );

            // Declarar o exchange e a fila para o tipo Headers
            await channel.ExchangeDeclareAsync(RabbitMqConstants.HeadersExchangeName, ExchangeType.Headers);
            await channel.QueueDeclareAsync(RabbitMqConstants.HeadersExchangeQueueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(
                RabbitMqConstants.HeadersExchangeQueueName,
                RabbitMqConstants.HeadersExchangeName,
                string.Empty,
                new Dictionary<string, object?>
                {
                    {RabbitMqConstants.HeadersKeyName, RabbitMqConstants.HeadersKeyValue },
                    {"x-match", "all" }
                }
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var headerValue = ea.BasicProperties.Headers?.FirstOrDefault().Value as byte[];
                var decodeHeaderValue = Encoding.UTF8.GetString(headerValue ?? Array.Empty<byte>());
                Console.WriteLine($"Exchange name: {ea.Exchange}, Routing Key: {ea.RoutingKey}, Headers: {decodeHeaderValue}");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine($"Received message: {message}");
                Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
                await Task.CompletedTask;
            };

            // Consumir mensagens das duas filas
            await channel.BasicConsumeAsync(RabbitMqConstants.QueueName, true, consumer);
            await channel.BasicConsumeAsync(RabbitMqConstants.TopicQueueName, true, consumer);
            await channel.BasicConsumeAsync(fanoutQueue, true, consumer);
            await channel.BasicConsumeAsync(RabbitMqConstants.HeadersExchangeQueueName, true, consumer);

            Console.WriteLine("Subscriber is running. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}