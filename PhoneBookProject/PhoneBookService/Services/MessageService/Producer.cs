using Confluent.Kafka;
using Newtonsoft.Json;
using PhoneBookService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookService.Services.MessageService
{
    public class Producer : IProducer
    {
        public async Task<string> PublishAsync(string topic, KafkaMessage message)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
            using (var p = new ProducerBuilder<string, string>(producerConfig).Build())
            {
                var messg = new Message<string, string> { Key = null, Value = JsonConvert.SerializeObject(message) };
                DeliveryResult<string, string> a = await p.ProduceAsync(topic, messg);
                return a.Key;
            }
        }
    }
}
