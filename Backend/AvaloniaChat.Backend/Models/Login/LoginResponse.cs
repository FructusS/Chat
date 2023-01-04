namespace AvaloniaChat.Backend.Models.Login
{
    public class LoginResponse
    {
        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}