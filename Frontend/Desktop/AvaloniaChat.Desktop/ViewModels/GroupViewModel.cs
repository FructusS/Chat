using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Desktop.Events;
using DynamicData.Tests;
using Prism.Commands;
using Prism.Events;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class GroupViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private const string url = "http://localhost:5000/api/Group";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
        };
        public DelegateCommand BackCommand { get; }


        private string _groupName;

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value; OnPropertyChanged();
            }
        }
        private Bitmap _groupLogo;

        public Bitmap GroupLogo
        {
            get => _groupLogo;
            set
            {
                _groupLogo = value; OnPropertyChanged();
            }
        }



        /// <summary>
        /// update group constructor
        /// </summary>
        /// <param name="groupDto"></param>
        /// <param name="eventAggregator"></param>
        public GroupViewModel(UpdateGroupDto groupDto, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            BackCommand = new DelegateCommand(OnBackButtonClick);
        }

        /// <summary>
        /// delete group constructor
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="eventAggregator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public GroupViewModel(Guid groupId, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            BackCommand = new DelegateCommand(OnBackButtonClick);
        }
        /// <summary>
        /// create group constructor
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public GroupViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            BackCommand = new DelegateCommand(OnBackButtonClick);

           // GroupLogo = new Bitmap("../")
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

        private void OnBackButtonClick()
        {
            _eventAggregator.GetEvent<NavigateToChatEvent>().Publish();
        }

        public static byte[] ImageToByteArrayFromFilePath(string imagefilePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagefilePath);
            return imageArray;
        }
    }
}
