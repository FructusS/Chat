﻿using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Backend.Hubs;
using AvaloniaChat.Backend.Services.Interfaces;
using AvaloniaChat.Domain.Models;
using AvaloniaChat.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaloniaChat.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IHubContext<ChatHub> _hubContext;
    public MessagesController(IMessageService messageService, IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _hubContext = hubContext;
    }

    [HttpGet("{groupId}")]
    public async Task<IActionResult> GetMessages(Guid groupId)
    {
        if (groupId == null)
        {
            return BadRequest("Invalid data");
        }


        var messagesList = await _messageService.GetMessages(groupId);
        return Ok(messagesList);
    }



    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
    {
        if (createMessageDto is null)
        {
            return BadRequest("Invalid data");
        }
        var message = await _messageService.CreateMessage(createMessageDto);
        await _hubContext.Clients.Group(message.GroupId.ToString()).SendAsync("ReceiveMessage", message);
        return Ok(message);
    }
}