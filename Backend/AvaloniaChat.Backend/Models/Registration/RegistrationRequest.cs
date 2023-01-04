namespace AvaloniaChat.Backend.Models.Registration
{
    public class RegistrationRequest
    {
        private string _username;
        private string _password;
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password { 
            get { return _password; } 
            set { _password = value; }
        }
    }
}
