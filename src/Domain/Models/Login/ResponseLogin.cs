namespace Domain.Models.Login
{
    public class ResponseLogin
    {
        public string UserName { get; private set; }
        public string Token { get; private set; }

        public ResponseLogin(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}
