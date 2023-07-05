using AvaloniaChat.Desktop.Models;
using Prism.Events;

namespace AvaloniaChat.Desktop.Events
{
    internal class NavigateToRegistrationEvent : PubSubEvent
    {
    }   
    internal class NavigateToLoginEvent : PubSubEvent
    {
    }
    internal class LoginEvent : PubSubEvent<UserModel>
    {
    } 
    internal class RegistrationEvent : PubSubEvent
    {
    }
}