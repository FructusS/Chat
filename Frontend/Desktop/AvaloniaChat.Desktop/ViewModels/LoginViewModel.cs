using Avalonia.Collections;
using Avalonia.Input;
using Avalonia.Remote.Protocol.Viewport;
using AvaloniaChat.Desktop.Commands;
using AvaloniaChat.Desktop.Events;
using AvaloniaChat.Desktop.Views;
using Prism.Commands;
using Prism.Events;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Desktop.Models;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private const string url = "http://localhost:5000/api/Auth";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
        };
        private readonly IEventAggregator _eventAggregator;

        private string _username;
        public string Username
        {
            get => _username;
            set {_username = value; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand NavigateToRegistrationCommand { get; }

        public LoginViewModel(IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            LoginCommand = new DelegateCommand(async () =>  OnLogin());
            NavigateToRegistrationCommand = new DelegateCommand(onNavigateToRegistration);
            OnLogin();
        }

        private async void OnLogin()
        {

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
            {
                return;
            }

            using StringContent jsonContent = new(

                JsonSerializer.Serialize(new AuthRequest()
                {
                    Username = Username,
                    Password = Password
                }),
                Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsync($"{url}/login", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    if (authResponse == null)
                    {
                        return;
                    }

                    UserModel.Token = authResponse.AccessToken;
                    UserModel.UserId = authResponse.UserId;
                    _eventAggregator.GetEvent<NavigateToChatEvent>().Publish();

                }
            }
            catch (Exception ex)
            {

            }

        }
        private void onNavigateToRegistration()
        {
            _eventAggregator.GetEvent<NavigateToRegistrationEvent>().Publish();
        }
    }
    
}
