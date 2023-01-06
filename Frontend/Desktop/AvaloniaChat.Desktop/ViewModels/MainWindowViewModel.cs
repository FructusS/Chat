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
            set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }
       

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            CurrentPage = new LoginViewModel(eventAggregator);
            eventAggregator.GetEvent<RegistrationEvent>().Subscribe(ToRegistration);
            //vm.RegistrationCommand.Subscribe(vm =>
            //{
            //    CurrentPage = new RegistrationViewModel();
            //});
            //CurrentPage = vm;

        }

        private void ToRegistration()
        {
            //var vm = new LoginViewModel();
            //CurrentPage = vm;
            CurrentPage = new RegistrationViewModel();
        }

    }
}
