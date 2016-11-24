using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class GetChatMembersCountParameters
    {
        [JsonProperty("chat_id")]
        public string ChatIdOrChannelUsername { get; set; }
    }
}
