using PhoneBookService.DataTransfer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookService.Services.MessageService
{
    public interface IProducer
    {
        Task<string> PublishAsync(string topic, KafkaMessage message);
    }
}
