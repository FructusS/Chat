using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.Group;

namespace AvaloniaChat.Desktop
{
    public class GroupRepository
    {
        private const string url = "http://localhost:5000/api/Group";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
        };

        public GroupRepository()
        {

        }

       // public async Task<GroupDto> CreateGroup()
    }
}
