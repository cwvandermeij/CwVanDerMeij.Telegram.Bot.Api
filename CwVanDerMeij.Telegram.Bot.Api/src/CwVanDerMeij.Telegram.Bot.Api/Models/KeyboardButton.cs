using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class KeyboardButton
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("request_contact")]
        public bool? RequestContact { get; set; }
        [JsonProperty("request_location")]
        public bool? RequestLocation { get; set; }
    }
}
