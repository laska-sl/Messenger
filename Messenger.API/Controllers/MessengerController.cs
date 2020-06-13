using System.Collections.Generic;

using Messenger.Data.Models;
using Messenger.Data.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Messenger.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : Controller
    {
        private readonly IMessageService messageService;


        public MessengerController(IMessageService messageService)
        {
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
    }
}
