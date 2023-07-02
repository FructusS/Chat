using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetMessages(int userGroupId); 
        Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto);
    }
}
