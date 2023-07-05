using AutoMapper;
using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Repository.Interfaces;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaChat.Infrastructure.Services.Implimentations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MessageDto>> GetMessages(Guid userGroupId)
        {
            return await _repository.GetMessages(userGroupId);    
        }

        public async Task<MessageDto> CreateMessage(CreateMessageDto createMessageDto)
        {
           var message = await _repository.CreateMessage(createMessageDto);
           
           return message;
        }
    }
}
