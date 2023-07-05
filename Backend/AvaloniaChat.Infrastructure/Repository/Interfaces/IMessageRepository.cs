using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Repository.Interfaces
{
    public interface IMessageRepository
    {
        public Task<List<MessageDto>> GetMessages(Guid userGroupId);
        public Task<MessageDto> CreateMessage(CreateMessageDto  createMessageDto);
    }
}
