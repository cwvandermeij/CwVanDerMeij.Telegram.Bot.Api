using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQuery
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("from")]
        public User From { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("offset")]
        public string Offset { get; set; }
    }
}
