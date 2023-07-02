using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Domain.Models;

namespace AvaloniaChat.Infrastructure.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task CreateGroup(Group group);
        Task DeleteGroup(Group group);
        Task UpdateGroup(Group group);
    }
}
