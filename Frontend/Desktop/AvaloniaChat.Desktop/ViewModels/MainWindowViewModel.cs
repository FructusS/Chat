using Avalonia.Controls;
using AvaloniaChat.Desktop.Commands;
using AvaloniaChat.Desktop.Events;
using Prism.Events;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentPage;
        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }


        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            CurrentPage = new ChatViewModel();
            eventAggregator.GetEvent<NavigateToRegistrationEvent>().Subscribe(ToRegistration);
            eventAggregator.GetEvent<LoginEvent>().Subscribe(ToLogin);

        }

        private void ToLogin()
        {
            CurrentPage = new ChatViewModel();
        }

        private void ToRegistration()
        {
            CurrentPage = new RegistrationViewModel();
        }

    }
}
