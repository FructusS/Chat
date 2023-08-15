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
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Desktop.Models;
using AvaloniaChat.Domain.Models;
using AvaloniaEdit.Editing;
using Prism.Events;
using ReactiveUI;
using static System.Net.Mime.MediaTypeNames;
using AvaloniaChat.Desktop.Events;

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

        private bool _isProgressVisisble;

        public bool IsProgressVisisble
        {
            get => _isProgressVisisble;
            set { _isProgressVisisble = value; OnPropertyChanged(); }
        }

        private bool _isProgressMessageVisisble;

        public bool IsProgressMessageVisisble
        {
            get => _isProgressMessageVisisble;
            set { _isProgressMessageVisisble = value; OnPropertyChanged(); }
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

        private string _groupTitle;

        public string GroupTitle
        {
            get => _groupTitle;
            set { _groupTitle = value; OnPropertyChanged(); }
        }

        private ObservableCollection<GroupDto> _userGroup = new();
        public ObservableCollection<GroupDto> UserGroups
        {
            get { return _userGroup; }
            set { _userGroup = value; OnPropertyChanged(); }
        }

        #endregion


        #region User profile

        private UserDto _userProfile;

        public UserDto UserProfile
        {
            get => _userProfile;
            set
            {
                _userProfile = value; OnPropertyChanged();
            }
        }

        #endregion
        #endregion

        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand SendCommand { get; }
        public DelegateCommand CreateGroup { get; }



        public ChatViewModel(IEventAggregator eventAggregator)
        {


            SendCommand = new DelegateCommand(OnSend);
            CreateGroup = new DelegateCommand(OnCreateGroup);


          
            _eventAggregator = eventAggregator;
            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl($"http://localhost:5000/chatHub", opt =>
                {
                    opt.AccessTokenProvider = () => Task.FromResult(UserModel.Token);
                })
                .Build();

            LoadUI();

            _hubConnection.On<MessageDto>("ReceiveMessage", (message) =>
            {
                Messages.Add(message);
            });

            Selection = new SelectionModel<GroupDto>();
            Selection.SelectionChanged += SelectionChanged;

        }

        private void OnCreateGroup()
        {
            _eventAggregator.GetEvent<NavigateToGroupEvent>().Publish();
        }

        private async Task LoadUI()
        {
            IsProgressVisisble = true;
            await Connect();
            await LoadUserGroups();
            await LoadMessageHistory();
            await LoadUserProfile();
            IsProgressVisisble = false;
        }

        private async Task LoadUserProfile()
        {
            UserProfile = await _httpClient.GetFromJsonAsync<UserDto>($"{baseUrl}/User/{UserModel.UserId}");
        }

        public SelectionModel<GroupDto> Selection { get; set; }
            
        private async void SelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs<GroupDto> e)
        {
            if (Selection.SelectedItem == null) return;
            IsProgressMessageVisisble = true;
            await LoadMessageHistory();
            IsProgressMessageVisisble = false;
        }

        private async Task LoadUserGroups()
        {
            var userGroup = await _httpClient.GetFromJsonAsync<List<GroupDto>>($"{baseUrl}/UserGroup/{UserModel.UserId}");

            UserGroups = new ObservableCollection<GroupDto>(userGroup);

            SelectedUserGroupIndex = 0;
        }

        private async Task LoadMessageHistory()
        {
            List<MessageDto?> messages = new();
            Messages.Clear();

            if (Selection.SelectedItem == null)
            {
                messages = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"{baseUrl}/Messages/{UserGroups[0].GroupId}");
                GroupTitle = UserGroups[0].GroupTitle;
            }
            else
            {
                messages = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"{baseUrl}/Messages/{Selection.SelectedItem.GroupId}");
                GroupTitle = Selection.SelectedItem.GroupTitle;
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
                UserId = UserModel.UserId
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
