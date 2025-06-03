namespace RabbitMq.Shared
{
    public class RabbitMqConstants
    {
        public const string ExchangeName = "direct_exchange";
        public const string QueueName = "direct_exchange_queue";
        public const string RoutingKey = "direct_exchange_routing_key";

        public const string TopicExchangeName = "topic_exchange";
        public const string TopicQueueName = "topic_exchange_queue";
        public const string TopicRoutingKeyInfo = "Messages.info";
        public const string TopicRoutingKeyWarning = "Messages.warning";
        public const string TopicRoutingKeyError = "Messages.error";

        public const string FanoutExchangeName = "fanout_exchange";

        public const string HeadersExchangeName = "headers_exchange";
        public const string HeadersExchangeQueueName = "headers_exchange_queue";
        public const string HeadersKeyName = "headers_exchange_key_name";
        public const string HeadersKeyValue = "headers_exchange_value";
    }
}
