using System.Collections.Generic;

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
        public IActionResult GetMessagesInInterval([FromQuery] DateIntervalParams dateIntervalParams)
        {
            var messages = this.messageService.GetMessagesInInterval(dateIntervalParams);

            return this.Ok(messages);
        }

        [HttpGet]
        [Route("{id}", Name = "GetMessageById")]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Returns message by given Id"
        )]
        public IActionResult GetMessageById(int id)
        {
            var message = this.messageService.GetMessageById(id);

            return this.Ok(message);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerOperation(
            Summary = "Creates new message"
        )]
        public IActionResult GetMessagesInInterval(MessageDTO messageDTO)
        {
            var message = this.mapper.Map<Message>(messageDTO);

            this.messageService.CreateNewMessage(message);

            return this.Created("GetMessage", message);
        }
    }
}
