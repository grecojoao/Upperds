using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class LoginResponse
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
