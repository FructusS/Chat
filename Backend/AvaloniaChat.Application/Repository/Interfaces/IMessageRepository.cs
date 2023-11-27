using AvaloniaChat.Application.DTO.Message;

namespace AvaloniaChat.Business.Repository.Interfaces
{
    public interface IMessageRepository
    {
        public Task<List<MessageDto>> GetMessages(Guid userGroupId);
        public Task<MessageDto> CreateMessage(CreateMessageDto  createMessageDto);
    }
}
