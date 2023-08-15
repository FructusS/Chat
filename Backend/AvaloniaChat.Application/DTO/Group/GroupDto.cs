using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Application.DTO.Group
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupTitle { get; set; }
        public byte[] GroupLogo { get; set; }
        public int UserGroupId { get; set; }
    }
}
