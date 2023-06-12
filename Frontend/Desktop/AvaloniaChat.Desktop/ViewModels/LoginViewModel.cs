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
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _login;
        public string Login
        {
            get => _login;
            set { value = _login; OnPropertyChanged(); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { value = _email; OnPropertyChanged(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { value = _password; OnPropertyChanged(); }
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand NavigateToRegistrationCommand { get; }

        public LoginViewModel(IEventAggregator eventAggregator)
        {

            _eventAggregator = eventAggregator;
            LoginCommand = new DelegateCommand(OnLogin);
            NavigateToRegistrationCommand = new DelegateCommand(onNavigateToRegistration);
        }

        private void OnLogin()
        {
            _eventAggregator.GetEvent<LoginEvent>().Publish();
        }
        private void onNavigateToRegistration()
        {
            _eventAggregator.GetEvent<NavigateToRegistrationEvent>().Publish();
        }
    }
    
}
