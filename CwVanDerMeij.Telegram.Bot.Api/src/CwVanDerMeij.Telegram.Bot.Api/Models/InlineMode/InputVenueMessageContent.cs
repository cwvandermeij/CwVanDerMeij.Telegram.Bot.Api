using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InputVenueMessageContent
    {
        [JsonProperty("latitude")]
        public float Latitude { get; set; }
        [JsonProperty("longitude")]
        public float Longitude { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("foursquare_id")]
        public string FoursquareId { get; set; }
    }
}
