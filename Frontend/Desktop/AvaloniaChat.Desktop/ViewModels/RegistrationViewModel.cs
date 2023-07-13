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
using Prism.Commands;
using Prism.Events;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private const string url = "http://localhost:5000/api/User";
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri(url),
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
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new CreateUserDto()
                {
                    Username = Username,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                }),
                Encoding.UTF8,
                "application/json");
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsync($"{url}/registration", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var authResponse = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (authResponse == null)
                    {
                        return;
                    }

                    _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void OnNavigateToLoginCommand()
        {
            _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();
        }
    }
}
