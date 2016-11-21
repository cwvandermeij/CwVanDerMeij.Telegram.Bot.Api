using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Venue
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("foursquare_id")]
        public string FoursquareId { get; set; }
    }
}
