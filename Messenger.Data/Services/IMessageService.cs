using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Messenger.Data.Models;

namespace Messenger.Data.Services
{
    public interface IMessageService
    {
        Task CreateNewMessage(Message message, CancellationToken cancellationToken);

        Task<Message> GetMessageById(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Message>> GetMessagesInInterval(DateIntervalParams dateIntervalParams, CancellationToken cancellationToken);
    }
}
