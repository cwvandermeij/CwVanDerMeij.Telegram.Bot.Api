using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class MessageEntity
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageEntityType Type { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("user")]
        public User User { get; set; } 
    }
}
