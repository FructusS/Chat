using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Desktop.Events;
using AvaloniaChat.Desktop.Views;
using DynamicData.Tests;
using Microsoft.IdentityModel.Tokens;
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
        public DelegateCommand ChangeGroupLogoCommand { get; }
        public DelegateCommand SaveGroupCommand { get; }


        private string _groupName;

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value; OnPropertyChanged();
            }
        }
        private byte[] _groupImage;

        public byte[] GroupImage
        {
            get => _groupImage;
            set
            {
                _groupImage = value; OnPropertyChanged();
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
            ChangeGroupLogoCommand = new DelegateCommand(OnChangeGroupLogo);
            SaveGroupCommand = new DelegateCommand(OnSaveGroup);
        }

        private void OnSaveGroup()
        {
            var group = new CreateGroupDto
            {
                GroupId = Guid.NewGuid(),
                GroupTitle = GroupName,
                GroupImage = GroupImage
            };
            
        }

        private async void OnChangeGroupLogo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string[]? result = await openFileDialog.ShowAsync(new MainWindow());
            if (result == null)
            {
                return;
            }
          
            GroupImage = ImageToByteArrayFromFilePath(result[0]);

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
            ChangeGroupLogoCommand = new DelegateCommand(OnChangeGroupLogo);
            SaveGroupCommand = new DelegateCommand(OnSaveGroup);
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
            ChangeGroupLogoCommand = new DelegateCommand(OnChangeGroupLogo);
            SaveGroupCommand = new DelegateCommand(OnSaveGroup);
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
