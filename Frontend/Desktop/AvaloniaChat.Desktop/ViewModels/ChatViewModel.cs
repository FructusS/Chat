using AvaloniaChat.Application.DTO.Message;
using AvaloniaChat.Domain.Models;
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

namespace AvaloniaChat.Desktop.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {

        private const string url = "http://localhost:5000/api/Messages/";
        private readonly HubConnection _hubConnection;
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
        };
        private ObservableCollection<MessageDto> messages = new();
        public ObservableCollection<MessageDto> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                OnPropertyChanged();
            }
        }

        private string messageText { get; set; }

        public string MessageText
        {
            get { return messageText; }
            set
            {
                messageText = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SendCommand { get; }

        public ChatViewModel()
        {
            SendCommand = new DelegateCommand(OnSend);



            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl($"http://localhost:5000/chatHub")
                .Build();

            Connect();
            LoadMessageHistory();
            _hubConnection.On<MessageDto>("ReceiveMessage", (message) =>
            {
                Messages.Add(message);
            });


        }

        private async void LoadMessageHistory()
        {
            var messages = await _httpClient.GetFromJsonAsync<List<MessageDto>>($"{url}{1}");
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
                UserGroupId = 1,
                UserId = 2
            };
            using StringContent jsonContent = new(

                JsonSerializer.Serialize(message),
                Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsync($"{url}create", jsonContent);
                MessageText = "";
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
