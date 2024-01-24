using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaChat.Application.DTO.User;
using AvaloniaChat.Desktop.Events;
using AvaloniaChat.Desktop.Models;
using AvaloniaChat.Desktop.Services;
using Prism.Commands;
using Prism.Events;
using Microsoft.AspNetCore.SignalR.Client;
using System.IO;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        

        private const string baseUrl = "http://localhost:5000/api/User";
        private readonly HubConnection _hubConnection;
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(baseUrl),
        };

        private string _username;
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(); }
        }


        public DelegateCommand NavigateToLoginCommand { get; }
        public DelegateCommand RegistrationCommand { get; }

        public RegistrationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            NavigateToLoginCommand = new DelegateCommand(OnNavigateToLoginCommand);
            RegistrationCommand = new DelegateCommand(async () => await OnRegistrationCommand());
        }

        private async Task OnRegistrationCommand()
        {
            var createUser = new CreateUserDto()
            {
                Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(createUser),
                Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage
                    response = await _httpClient.PostAsync($"{baseUrl}/registration", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (authResponse != null)
                    {
                        _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();
                    }
                }

            }
            catch (IOException ex)
            {
                throw new IOException($"{ex.Message}");

            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong. Try later");
            }
        }

        private void OnNavigateToLoginCommand()
        {
            _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();
        }
    }
}
