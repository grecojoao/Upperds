using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class TreeResponse : EntityResponse
    {
        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("speciesId")]
        public int? SpeciesId { get; set; }

        [JsonProperty("groupTreesId")]
        public int? GroupTreesId { get; set; }

        [JsonProperty("species")]
        public SpeciesResponse SpeciesResponse { get; set; }

        [JsonProperty("groupTrees")]
        public GroupTreesResponse GroupTreesResponse { get; set; }
    }
}
