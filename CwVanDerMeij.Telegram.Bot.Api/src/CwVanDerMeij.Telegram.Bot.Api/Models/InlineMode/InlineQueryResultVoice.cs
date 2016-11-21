using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQueryResultVoice
    {
        [JsonProperty("type")]
        public InlineQueryResultType Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("voice_url")]
        public string VoiceUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("voice_duration")]
        public int VoiceDuration { get; set; }
        [JsonProperty("reply_markup")]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
        [JsonProperty("input_message_content")]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
