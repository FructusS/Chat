using AutoMapper;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Business.Repository.Interfaces;
using AvaloniaChat.Data;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Business.Repository.Implimentations
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

        public async Task<List<MessageDto>> GetMessages(Guid groupId)
        {
            return await _context.Messages.Where(x => x.GroupId == groupId).Select(x=> new MessageDto
            {
                Username = x.User.Username,
                SendDate = x.SendDate,
                MessageText = x.MessageText
            }).ToListAsync();
        }

        public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto)
        {
            var user = await _context.Users.FirstAsync(x => x.UserId == createMessageDto.UserId);
            var message = _context.Messages.Add(new Message
            {
                SendDate = createMessageDto.SendDate,
                MessageText = createMessageDto.MessageText,
                GroupId = createMessageDto.GroupId,
                User = user,
                UserId = createMessageDto.UserId
            });

            await _context.SaveChangesAsync();

            return _mapper.Map<MessageDto>(message.Entity);
        }


    }
}
