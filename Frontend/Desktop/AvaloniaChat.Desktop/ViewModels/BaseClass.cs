using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaChat.Desktop.ViewModels
{
    public class BaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(sender:this, e:new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainViewmodel : BaseClass
    {
        public object CurrentPage { get; set; }
        
        public MainViewmodel() 
        { 
            CurrentPage = new RegistrationViewmodel();
        }
    }
    public class RegistrationViewmodel : BaseClass
    {

    }
}