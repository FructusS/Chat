using Newtonsoft.Json;

namespace AvaloniaChat.Backend.Models.AuthModels
{
    public class AuthRequest
    {
        private string _password;
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}