using System;
using System.Threading.Tasks;

using AutoMapper;

using Messenger.Data.DTOs;
using Messenger.Data.Models;
using Messenger.Data.Services;

using Microsoft.AspNetCore.SignalR;

namespace Messenger.API.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMapper mapper;

        private readonly IMessageService messageService;

        public MessageHub(IMapper mapper, IMessageService messageService)
        {
            this.mapper = mapper;
            this.messageService = messageService;
        }

        public async Task NewMessage(MessageDTO messageDTO)
        {
            var message = this.mapper.Map<Message>(messageDTO);

            message.CreatedAt = DateTime.Now;

            try
            {
                this.messageService.CreateNewMessage(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await this.Clients.All.SendAsync("NewMessage", message);
        }
    }
}
