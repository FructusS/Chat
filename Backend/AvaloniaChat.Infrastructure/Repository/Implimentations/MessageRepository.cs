using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure.Repository.Implimentations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(ChatDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> GetMessages(int userGroupId)
        {
            return await _context.Messages.Where(x => x.UsergroupId == userGroupId).Select(x=> new MessageDto
            {
                Username = x.Usergroup.User.Username,
                SendDate = x.SendDate,
                UsergroupId = x.UsergroupId,
                MessageText = x.MessageText
            }).ToListAsync();
        }

        public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto)
        {
            
            var message = _context.Messages.Add(new Message
            {
                SendDate = createMessageDto.SendDate,
                MessageText = createMessageDto.MessageText,
                UsergroupId = createMessageDto.UserGroupId,
                UserId = createMessageDto.UserId,
            });

            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDto>(message.Entity);
        }

    }
}
