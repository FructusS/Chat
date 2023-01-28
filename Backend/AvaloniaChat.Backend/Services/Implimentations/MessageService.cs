using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Models;
using AvaloniaChat.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Backend.Services.Implimentations
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _chatDbContext;

        public MessageService(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public Task<List<Message>> GetMessages(int userGroupId)
        {
            return _chatDbContext.Messages.Take(20).ToListAsync();
        }
    }
}
