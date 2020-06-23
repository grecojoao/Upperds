using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class EntityResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
