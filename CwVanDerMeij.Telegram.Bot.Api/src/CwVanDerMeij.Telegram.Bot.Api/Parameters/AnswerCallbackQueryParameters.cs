using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class AnswerCallbackQueryParameters
    {
        [JsonProperty("callback_query_id")]
        public string CallbackQueryId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("show_alert")]
        public bool? ShowAlert { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("cache_time")]
        public int? CacheTime { get; set; }
    }
}
