using System;
using Domain.Models;
using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class HarvestTreeResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("grossWeight")]
        public double GrossWeight { get; set; }

        [JsonProperty("information")]
        public string Information { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; } = false;

        [JsonProperty("treeId")]
        public int? TreeId { get; set; }

        [JsonProperty("tree")]
        public Tree Tree { get; set; }
    }
}
