using Avalonia.Controls;
using AvaloniaChat.Desktop.Commands;
using AvaloniaChat.Desktop.Events;
using Prism.Events;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using AvaloniaChat.Application.DTO.Group;
using AvaloniaChat.Desktop.Models;

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

        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CurrentPage = new LoginViewModel(_eventAggregator);
            _eventAggregator.GetEvent<NavigateToRegistrationEvent>().Subscribe(ToRegistration);
            _eventAggregator.GetEvent<LoginEvent>().Subscribe(ToLogin);
            _eventAggregator.GetEvent<NavigateToGroupEvent>().Subscribe(ToCreateGroup);
        }

        private void ToCreateGroup()
        {
            CurrentPage = new GroupViewModel();
        }


        //private void ToDeleteGroup(Guid groupDtoId)
        //{
        //    CurrentPage = new GroupViewMdel(groupDtoId);
        //}

        private void ToUpdateGroup(UpdateGroupDto updateGroupDto)
        {
            CurrentPage = new GroupViewModel(updateGroupDto);
        }

        private void ToLogin(UserModel userModel)
        {
            CurrentPage = new ChatViewModel(userModel, _eventAggregator);

        }

        private void ToRegistration()
        {
            CurrentPage = new RegistrationViewModel();
        }

    }
}
