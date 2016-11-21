using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineQueryResultVenue
    {
        [JsonProperty("type")]
        public InlineQueryResultType Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("latitude")]
        public float Latitude { get; set; }
        [JsonProperty("longitude")]
        public float Longitude { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("foursquare_id")]
        public string FoursquareId { get; set; }
        [JsonProperty("reply_markup")]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
        [JsonProperty("input_message_content")]
        public InputMessageContent InputMessageContent { get; set; }
        [JsonProperty("thumb_url")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("thumb_width")]
        public int ThumbnailWidth { get; set; }
        [JsonProperty("thumb_height")]
        public int ThumbnailHeight { get; set; }
    }
}
