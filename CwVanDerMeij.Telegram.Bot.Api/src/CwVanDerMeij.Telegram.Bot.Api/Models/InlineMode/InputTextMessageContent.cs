using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InputTextMessageContent
    {
        [JsonProperty("message_text")]
        public string MessageText { get; set; }
        [JsonProperty("parse_mode")]
        public string ParseMode { get; set; }
        [JsonProperty("disable_web_page_preview")]
        public bool? DisableWebPagePreview { get; set; }
    }
}
