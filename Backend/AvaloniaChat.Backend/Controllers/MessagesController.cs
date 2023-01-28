using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaloniaChat.Backend.Controllers
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

        [HttpGet("{groupId}")]
        public async Task<List<Message>> GetMessages(int groupId)
        {

            return await _messageService.GetMessages(groupId);
        }

       
    }
}
