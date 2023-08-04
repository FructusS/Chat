using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Desktop.Events;
using AvaloniaChat.Desktop.Models;
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
        private const string baseUrl = "http://localhost:5000/api/Group";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(baseUrl),
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
            SaveGroupCommand = new DelegateCommand(async () => OnSaveGroup());
        }

        private async void OnSaveGroup()
        {
            var createGroup = new CreateGroupDto
            {
                GroupId = Guid.NewGuid(),
                GroupTitle = GroupName,
                GroupImage = GroupImage
            };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(createGroup),
                Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage
                    response = await _httpClient.PostAsync($"{baseUrl}/create", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var group = await response.Content.ReadFromJsonAsync<GroupDto>();
                    if (group != null)
                    {
                        _eventAggregator.GetEvent<NavigateToChatEvent>().Publish();

                        //var userGroup = new CreateUserGroup()
                        //{

                        //};
                    }
                }

            }
            catch (Exception ex)
            {

            }


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
