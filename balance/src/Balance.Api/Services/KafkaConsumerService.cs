using Confluent.Kafka;
using BalanceApi.Data;
using Newtonsoft.Json;

namespace BalanceApi.Services
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _topic = "balances";
        private readonly string _groupId = "wallet";
        // private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _bootstrapServers = "kafka:29092";

        public KafkaConsumerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
        }

        private async void StartConsumerLoop(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {

                try
                {
                    var consumeResult = consumer.Consume(cancellationToken);

                    var messageWrapper = JsonConvert.DeserializeObject<MessageWrapper>(consumeResult.Message.Value);

                    Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'");

                    if (messageWrapper == null)
                    {
                        continue;
                    }
                    Console.WriteLine($"messasgeWrapper: {messageWrapper}");

                    var message = messageWrapper.Payload;

                    if (message == null)
                    {
                        continue;
                    }

                    Console.WriteLine($"message: {message}");

                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<BalanceContext>();

                    var accountFrom = await context.Accounts.FindAsync(message.account_id_from);

                    Console.WriteLine($"accountFrom: {accountFrom}");

                    if (accountFrom != null)
                    {
                        accountFrom.Balance = message.balance_account_id_from;
                        accountFrom.Updated_At = DateTime.UtcNow;
                    }

                    var accountTo = await context.Accounts.FindAsync(message.account_id_to);
                    if (accountTo != null)
                    {
                        accountTo.Balance = message.balance_account_id_to;
                        accountTo.Updated_At = DateTime.UtcNow;
                    }

                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                }
            }
        }

    }

    public class MessageWrapper
    {
        public string Name { get; set; }
        public KafkaMessage Payload { get; set; }
    }

    public class KafkaMessage
    {
        public string account_id_from { get; set; }
        public string account_id_to { get; set; }
        public int balance_account_id_from { get; set; }
        public int balance_account_id_to { get; set; }
    }
}
