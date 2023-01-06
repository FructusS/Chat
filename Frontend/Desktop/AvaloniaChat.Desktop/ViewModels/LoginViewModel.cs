using Avalonia.Collections;
using Avalonia.Input;
using Avalonia.Remote.Protocol.Viewport;
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

namespace AvaloniaChat.Desktop.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _login;
        public string Login
        {
            get { return _login; }
            set { this.RaiseAndSetIfChanged(ref _login, value); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public DelegateCommand RegistrationCommand { get; }
        public DelegateCommand LoginCommand { get; }
        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegistrationCommand = new DelegateCommand(ToRegistration);
            LoginCommand = new DelegateCommand(OnLogin);
        }

        private void OnLogin()
        {
        }
        private void ToRegistration()
        {

            _eventAggregator.GetEvent<RegistrationEvent>().Publish();
        }


    }
}
