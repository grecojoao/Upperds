namespace Domain.Models.Login
{
    public class UserDto
    {
        public string UserName { get; private set; }
        public string Token { get; private set; }

        public UserDto(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}
