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

namespace AvaloniaChat.Desktop.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private UserService _userService;

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

        public RegistrationViewModel(IEventAggregator eventAggregator, UserService userService)
        {
            _eventAggregator = eventAggregator;
            _userService = userService;
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

            var user = await _userService.RegistrationUser(createUser);
            if (user.IsError == false)
            {
                _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();
            }
        }

        private void OnNavigateToLoginCommand()
        {
            _eventAggregator.GetEvent<NavigateToLoginEvent>().Publish();
        }
    }
}
