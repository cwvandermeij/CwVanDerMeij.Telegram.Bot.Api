using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQueryResultVideo
    {
        [JsonProperty("type")]
        public InlineQueryResultType Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("video_width")]
        public int VideoWidth { get; set; }
        [JsonProperty("video_height")]
        public int VideoHeight { get; set; }
        [JsonProperty("video_duration")]
        public int VideoDuration { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("reply_markup")]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
        [JsonProperty("input_message_content")]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
