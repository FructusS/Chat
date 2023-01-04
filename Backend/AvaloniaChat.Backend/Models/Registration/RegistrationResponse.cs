namespace AvaloniaChat.Backend.Models.Registration
{
    public class RegistrationResponse
    {
        private string _username;
        private string _email;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
   

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}