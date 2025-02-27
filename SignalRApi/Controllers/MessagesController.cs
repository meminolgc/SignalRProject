using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDto;
using SignalR.EntityLayer.Entities;
using System.Runtime.CompilerServices;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MessagesController : ControllerBase
	{
		private readonly IMessageService _messageService;

		public MessagesController(IMessageService messageService)
		{
			_messageService = messageService;
		}

		[HttpGet]
		public IActionResult MessageList()
		{
			var values = _messageService.TGetListAll();
			return Ok(values);
		}
		[HttpPost]
		public IActionResult CreateMessage(CreateMessageDto createMessageDto)
		{
			Message message = new()
			{
				Mail = createMessageDto.Mail,
				MessageContent = createMessageDto.MessageContent,
				MessageSendDate = DateTime.Now,
				NameSurname = createMessageDto.NameSurname,
				Status = false,
				Subject = createMessageDto.Subject,
				Phone = createMessageDto.Phone
			};
			_messageService.TAdd(message);
			return Ok("Mesaj Başarılı Bir Şekilde Eklendi");
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteMessage(int id)
		{
			var value = _messageService.TGetByID(id);
			_messageService.TDelete(value);
			return Ok("Mesaj Silindi");
		}
		[HttpPut]
		public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
		{
			Message Message = new()
			{
				MessageID = updateMessageDto.MessageID,
				Mail = updateMessageDto.Mail,
				MessageContent = updateMessageDto.MessageContent,
				MessageSendDate = DateTime.Now,
				NameSurname =updateMessageDto.NameSurname,
				Status = false,
				Subject = updateMessageDto.Subject,
				Phone = updateMessageDto.Phone
			};
			_messageService.TUpdate(Message);
			return Ok("Mesaj Güncellendi");
		}
		[HttpGet("{id}")]
		public IActionResult GetMessage(int id)
		{
			var value = _messageService.TGetByID(id);
			return Ok(value);
		}
	}
}
