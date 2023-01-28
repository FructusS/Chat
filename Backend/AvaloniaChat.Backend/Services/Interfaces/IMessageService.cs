using AvaloniaChat.Backend.Models;

namespace AvaloniaChat.Backend.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessages(int userGroupId);
    }
}
