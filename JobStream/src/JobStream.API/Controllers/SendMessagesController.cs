using JobStream.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobStream.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SendMessagesController : ControllerBase
	{
		private readonly ISendMessageService _sendMessageService;

        public SendMessagesController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }
    }
}
