using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Messenger.Data.DTOs;
using Messenger.Data.Models;
using Messenger.Data.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using AutoMapper;

namespace Messenger.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public MessengerController(IMessageService messageService, IMapper mapper)
        {
            this.mapper = mapper;
            this.messageService = messageService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Returns messages in given date/datetime interval"
        )]
        public async Task<IActionResult> GetMessagesInInterval([FromQuery] DateIntervalParams dateIntervalParams, CancellationToken cancellationToken)
        {
            var messages = await this.messageService.GetMessagesInInterval(dateIntervalParams, cancellationToken);

            return this.Ok(messages);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMessageById")]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "Returns message by given Id"
        )]
        public async Task<IActionResult> GetMessageById(int id, CancellationToken cancellationToken)
        {
            var message = await this.messageService.GetMessageById(id, cancellationToken);

            if (message != null)
            {
                return this.Ok(message);
            }

            return this.NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(
            Summary = "Creates new message"
        )]
        public async Task<IActionResult> GetMessagesInInterval(MessageDTO messageDTO, CancellationToken cancellationToken)
        {
            var message = this.mapper.Map<Message>(messageDTO);

            message.CreatedAt = DateTime.Now;

            await this.messageService.CreateNewMessage(message, cancellationToken);

            return this.Created("GetMessage", message);
        }
    }
}
