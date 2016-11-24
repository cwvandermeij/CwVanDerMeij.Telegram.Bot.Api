using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class ForwardMessageParameters
    {
        [JsonProperty("chat_id")]
        public string ChatIdOrChannelUsername { get; set; }
        [JsonProperty("from_chat_id")]
        public string FromChatIdOrChannelUsername { get; set; }
        [JsonProperty("disable_notification")]
        public bool? DisableNotification { get; set; }
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
    }
}
