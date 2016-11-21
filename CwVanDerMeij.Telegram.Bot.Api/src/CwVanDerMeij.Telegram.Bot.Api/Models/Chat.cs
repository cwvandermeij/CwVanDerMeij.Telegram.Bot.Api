using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Chat
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChatType Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("all_members_are_administrators")]
        public bool? AllMembersAreAdministrators { get; set; }
    }
}
