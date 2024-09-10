using Confluent.Kafka;
using System.Text;

namespace FileProcessApp.Common
{
    public class MessageBrokerService
    {
        private ProducerConfig _config;
        private string _topic;
        private readonly IConfiguration _configuration;

        public MessageBrokerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _topic = "ProcessFile";
        }

        public void Produce(string key, string message)
        {
            var con = _configuration.GetSection("EventBrokerConfig:brokerList").Value;
            _config = new ProducerConfig { BootstrapServers = con };
            using (var producer = new ProducerBuilder<string, string>(_config).Build())
            {
                try
                {
                    var messages = new Message<string, string>
                    {
                        Key = key,
                        Value = message,
                        Headers = new Headers()
                    };

                    var deliveryReport = producer.ProduceAsync(_topic, messages).Result;

                    Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
