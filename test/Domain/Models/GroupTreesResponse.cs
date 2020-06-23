using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class GroupTreesResponse : EntityResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
