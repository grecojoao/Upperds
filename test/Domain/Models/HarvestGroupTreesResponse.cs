using System;
using Domain.Models;
using Newtonsoft.Json;

namespace test.Domain.Models
{
    public class HarvestGroupTreesResponse
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

        [JsonProperty("groupId")]
        public int? GroupId { get; set; }

        [JsonProperty("group")]
        public GroupTrees Group { get; set; }
    }
}
