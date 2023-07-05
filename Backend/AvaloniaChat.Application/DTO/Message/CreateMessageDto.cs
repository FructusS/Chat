using AvaloniaChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Application.DTO.Message
{
    public class CreateMessageDto
    {
        public Guid GroupId { get; set; }
        public string MessageText { get; set; } = null!;
        public int UserId { get; set; }

        public DateTime SendDate { get; set; }

    }
}
