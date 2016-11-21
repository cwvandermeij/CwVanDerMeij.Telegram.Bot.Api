using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQueryResultGif
    {
        [JsonProperty("type")]
        public InlineQueryResultType Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("gif_url")]
        public string GifUrl { get; set; }
        [JsonProperty("gif_width")]
        public int GifWidth { get; set; }
        [JsonProperty("gif_height")]
        public int GifHeight { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("reply_markup")]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
        [JsonProperty("input_message_content")]
        public InputMessageContent InputMessageContent { get; set; }
    }
}
