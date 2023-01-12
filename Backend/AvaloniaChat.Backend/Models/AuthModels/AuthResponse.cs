namespace AvaloniaChat.Backend.Models.AuthModels
{
    public class AuthResponse
    {
        private string _accesstoken;
        //      private string _refreshstoken;

        public string AccessToken
        {
            get { return _accesstoken; }
            set { _accesstoken = value; }
        }
        //public string RefreshToken
        //{
        //    get { return _refreshstoken; }
        //    set { _refreshstoken = value; }
        //}
    }
}