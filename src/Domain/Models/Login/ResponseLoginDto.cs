namespace Domain.Models.Login
{
    public class ResponseLoginDto
    {
        public string UserName { get; private set; }
        public string Token { get; private set; }

        public ResponseLoginDto(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}
