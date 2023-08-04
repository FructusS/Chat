using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Application.DTO.UserGroup
{
    public class CreateUserGroupDto
    {
        public Guid GroupId { get; set;}
        public int UserId { get; set;}
    }
}
