using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Application.DTO.Group
{
    public class CreateGroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; }
        public byte[]? GroupImage { get; set; }
    }
}
