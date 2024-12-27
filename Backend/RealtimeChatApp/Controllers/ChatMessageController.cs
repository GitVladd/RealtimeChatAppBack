using Microsoft.AspNetCore.Mvc;
using RealtimeChatApp.Dtos;
using RealtimeChatApp.QueryParams;
using RealtimeChatApp.Services;

namespace RealtimeChatApp.Controllers
{
	[ApiController]
	[Route("/api")]
	public class ChatMessageController : ControllerBase
	{
		private readonly IChatMessageService _chatMessageService;

		public ChatMessageController(IChatMessageService chatMessageService)
		{
			_chatMessageService = chatMessageService;
        }

		[HttpGet]
		[Route("chatmessages")]
		public async Task<ActionResult<IEnumerable<ChatMessageGetDto>>> GetMessages([FromQuery] PaginationParameter paginationParameter)
		{
			var lastmessages = await _chatMessageService.GetMessagesAsync(paginationParameter);

            return Ok(lastmessages);
		}
	}
}
