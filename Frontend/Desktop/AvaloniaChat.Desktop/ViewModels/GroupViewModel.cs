using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Application.DTO.Group;
using DynamicData.Tests;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class GroupViewModel : ViewModelBase
    {
        private const string url = "http://localhost:5000/api/Group";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
        };
        public  GroupViewModel()
        {
            Test();
           
        }

        private async void Test()
        {
            var group = new CreateGroupDto()
            {
                GroupId = Guid.NewGuid(),
                GroupImage = ImageToByteArrayFromFilePath("seats.jpg"),
                GroupTitle = "group with image"
            };
            using StringContent jsonContent = new(

                JsonSerializer.Serialize(group),
                Encoding.UTF8,
                "application/json");
            using HttpResponseMessage response = await _httpClient.PostAsync($"{url}/create", jsonContent);

            
        }

        public GroupViewModel(UpdateGroupDto updateGroupDto)
        {
            throw new NotImplementedException();
        }

        public GroupViewModel(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public static byte[] ImageToByteArrayFromFilePath(string imagefilePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagefilePath);
            return imageArray;
        }
    }
}
