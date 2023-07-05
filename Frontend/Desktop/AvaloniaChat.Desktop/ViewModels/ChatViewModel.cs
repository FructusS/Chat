using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Application.DTO.UserGroup;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Desktop.Models;
using AvaloniaChat.Domain.Models;
using AvaloniaEdit.Editing;
using ReactiveUI;
using static System.Net.Mime.MediaTypeNames;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {

        private const string baseUrl = "http://localhost:5000/api";
        private readonly HubConnection _hubConnection;
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(baseUrl),
        };


        #region fields

        private string _messageText;

        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; OnPropertyChanged(); }
        }
        private int _selectedUserGroupIndex;

        public int SelectedUserGroupIndex
        {
            get { return _selectedUserGroupIndex; }
            set { _selectedUserGroupIndex = value; OnPropertyChanged(); }
        }


        #region Messages

        private ObservableCollection<MessageDto> _messages = new();
        public ObservableCollection<MessageDto> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); }
        }

        #endregion

        #region User group

        private ObservableCollection<GroupDto> _userGroup = new();
        public ObservableCollection<GroupDto> UserGroups
        {
            get { return _userGroup; }
            set { _userGroup = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion






        private UserModel _userModel;
        public DelegateCommand SendCommand { get; }
        public ChatViewModel(UserModel userModel)
        {
            SendCommand = new DelegateCommand(OnSend);


            _userModel = userModel;
            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl($"http://localhost:5000/chatHub", opt =>
                {
                    opt.AccessTokenProvider = () => Task.FromResult(_userModel.Token);
                })
                .Build();
            Task.Run(() =>
            {
                Connect();
                LoadUserGroups();

            });


            _hubConnection.On<MessageDto>("ReceiveMessage", (message) =>
            {
                Messages.Add(message);
            });



            Selection = new SelectionModel<GroupDto>();
            Selection.SelectionChanged += SelectionChanged;


        }

        public SelectionModel<GroupDto> Selection { get; set; }

        private void SelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs<GroupDto> e)
        {
            if (Selection.SelectedItem != null)
            {
                LoadMessageHistory();
            }
        }

        private async Task LoadUserGroups()
        {
            var userGroup = await _httpClient.GetFromJsonAsync<List<GroupDto>>($"{baseUrl}/Group/{_userModel.UserId}");

            UserGroups = new ObservableCollection<GroupDto>(userGroup);

            SelectedUserGroupIndex = 0;

            LoadMessageHistory();

        }

        private async void LoadMessageHistory()
        {
            List<MessageDto?> messages = new();
            Messages.Clear();

            if (Selection.SelectedItem == null)
            {
                messages = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"{baseUrl}/Messages/{UserGroups[0].GroupId}");

            }
            else
            {
                messages = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"{baseUrl}/Messages/{Selection.SelectedItem.GroupId}");
            }

            Messages = new ObservableCollection<MessageDto>(messages);
        }
        public async Task Connect()
        {
            try
            {
                await this._hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageText = $"{ex.Message}";
            }
        }

        private async void OnSend()
        {

            if (string.IsNullOrEmpty(MessageText))
                return;

            var message = new CreateMessageDto()
            {
                SendDate = DateTime.Now,
                MessageText = MessageText,
                GroupId = Selection.SelectedItem.GroupId,
                UserId = _userModel.UserId
            };

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(message),
                Encoding.UTF8,
                "application/json");

            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsync($"{baseUrl}/Messages/create", jsonContent);
                MessageText = string.Empty;
            }
            catch (Exception e)
            {

                switch (e)
                {
                    case IOException:

                        break;
                    case TimeoutException:

                        break;

                    default:
                        break;
                }

            }

        }
    }
}
