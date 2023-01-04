using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public object CurrentPage { get; set; }

        public MainWindowViewModel()
        {
            CurrentPage = new RegistrationViewModel();
        }

    }
}
