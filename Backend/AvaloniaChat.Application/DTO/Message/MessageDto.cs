using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Application.DTO.Message
{
    public class MessageDto
    {
        
        public Guid MessageId { get; set; }
        public Guid GroupId { get; set; }
        public string MessageText { get; set; } = null!;
        public string Username { get; set; }

        public DateTime SendDate { get; set; }
    }
}
