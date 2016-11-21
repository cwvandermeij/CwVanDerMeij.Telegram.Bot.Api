using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQueryResultArticle
    {
        [JsonProperty("type")]
        public InlineQueryResultType Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("input_message_content")]
        public InputMessageContent InputMessageContent { get; set; }
        [JsonProperty("reply_markup")]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("hide_url")]
        public bool HideUrl { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("thumb_width")]
        public int ThumbnailWidth { get; set; }
        [JsonProperty("thumb_height")]
        public int ThumbnailHeight { get; set; }
    }
}
