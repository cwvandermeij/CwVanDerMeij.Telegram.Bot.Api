using CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Update
    {
        [JsonProperty("update_id")]
        public int UpdateId { get; set; }
        [JsonProperty("message")]
        public Message Message { get; set; }
        [JsonProperty("edited_message")]
        public Message EditedMessage { get; set; }
        [JsonProperty("inline_query")]
        public InlineQuery InlineQuery { get; set; }
        [JsonProperty("chosen_inline_result")]
        public ChosenInlineResult ChosenInlineResult { get; set; }
        [JsonProperty("callback_query")]
        public CallbackQuery CallbackQuery { get; set; }
    }
}
