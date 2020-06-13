using System.Collections.Generic;

using Messenger.Data.Models;

namespace Messenger.Data.Services
{
    public interface IMessageService
    {
        void CreateNewMessage(Message message);

        Message GetMessageById(int id);

        IEnumerable<Message> GetAllMessages();

        IEnumerable<Message> GetMessagesInInterval(DateIntervalParams dateIntervalParams);
    }
}
