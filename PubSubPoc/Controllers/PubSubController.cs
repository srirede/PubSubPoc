using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueuingService;
using System.Net;

namespace PubSubPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PubSubController : ControllerBase
    {
        private readonly IPubSubQueuing _pubSubQueuing;
        public PubSubController(IPubSubQueuing pubSubQueuing)
        {
            _pubSubQueuing = pubSubQueuing;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(SendMessageInput sendMessageInput)
        {
            try
            {
                PubsubMessage message = new PubsubMessage
                {
                    Data = ByteString.CopyFromUtf8(sendMessageInput.Message),
                    Attributes = { { "FromAPI", sendMessageInput.Message } }
                };
                var messageId = await _pubSubQueuing.SendMessage(message);
                return Ok(messageId);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

    public class SendMessageInput
    {
        public string Message { get; set; }
    }
}
