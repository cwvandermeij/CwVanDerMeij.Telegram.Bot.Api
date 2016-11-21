using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Location
    {
        [JsonProperty("longitude")]
        public float Longitude { get; set; }
        [JsonProperty("latitude")]
        public float Latitude { get; set; }
    }
}
