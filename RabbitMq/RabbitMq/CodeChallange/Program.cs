using RabbitMq.CodeChallange;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallange
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("RabbitMq Code Challange");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(RabbitMqConstants.FanoutQueueName, ExchangeType.Fanout);
            await channel.QueueDeclareAsync(RabbitMqConstants.FanoutQueueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(RabbitMqConstants.FanoutQueueName, RabbitMqConstants.FanoutExchangeName, "");

            const string resultExchangeName = "result.exchange";
            const string resultQueueName = "result.queue";

            await channel.ExchangeDeclareAsync(resultExchangeName, ExchangeType.Fanout);
            await channel.QueueDeclareAsync(resultQueueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(resultQueueName, resultExchangeName, "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                string result;
                if (int.TryParse(message, out int number))
                {
                    result = number % 2 == 0 ? "Success" : "Failure";
                }
                else
                {
                    result = "Invalid input";
                }

                var resultMessage = Encoding.UTF8.GetBytes($"Processed: '{message}': {result}");
                await channel.BasicPublishAsync(resultExchangeName, string.Empty, body: resultMessage);
            };

            await channel.BasicConsumeAsync(RabbitMqConstants.FanoutQueueName, autoAck: true, consumer: consumer);

            var resultConsumer = new AsyncEventingBasicConsumer(channel);
            resultConsumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Result: {message}");
                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(resultQueueName, autoAck: true, consumer: resultConsumer);

            Console.WriteLine("Enter a number to publish");

            while (true)
            {
                var input = Console.ReadLine();

                var body = Encoding.UTF8.GetBytes(input);
                await channel.BasicPublishAsync(RabbitMqConstants.FanoutExchangeName, string.Empty, body: body);

                Console.WriteLine($"Publisher sent {input}");
            }

        }
    }
}
