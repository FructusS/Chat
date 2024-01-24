using AvaloniaChat.Desktop.Events;
using Prism.Commands;
using Prism.Events;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AvaloniaChat.Application.DTO.Auth;
using AvaloniaChat.Backend.Controllers;
using AvaloniaChat.Data.DTO.Auth;
using AvaloniaChat.Desktop.Models;
using AvaloniaChat.Models.Response;

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
        private string _errorText;
        public string ErrorText
        {
            get => _errorText;
            set { _errorText = value; OnPropertyChanged(); }
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
            ErrorText = "1212321312";
            LoginCommand = new DelegateCommand(async () =>  OnLogin());
            NavigateToRegistrationCommand = new DelegateCommand(onNavigateToRegistration);
            OnLogin();
        }

        private async void OnLogin()
        {

            ErrorText = string.Empty;
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
                var authResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (response.IsSuccessStatusCode)
                {
                    if (authResponse == null)
                    {
                        return;
                    }
                    var bodyResponse = authResponse.Data as AuthResponse;

                    if (authResponse.Success && bodyResponse != null)
                    {

                        UserModel.Token = bodyResponse.AccessToken;
                        UserModel.UserId = bodyResponse.UserId;
                        _eventAggregator.GetEvent<NavigateToChatEvent>().Publish();
                    }
                    else
                    {
                        ErrorText = authResponse.Error.Message;
                    }


                }
                else
                {
                    ErrorText = authResponse.Error.Message;
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

    record class LoginResponse{}
    {
    }
}
