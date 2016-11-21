using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class ResponseParameters
    {
        [JsonProperty("migrate_to_chat_id")]
        public long MigrateToChatId { get; set; }
        [JsonProperty("reply_after")]
        public int? ReplyAfter { get; set; }
    }
}
