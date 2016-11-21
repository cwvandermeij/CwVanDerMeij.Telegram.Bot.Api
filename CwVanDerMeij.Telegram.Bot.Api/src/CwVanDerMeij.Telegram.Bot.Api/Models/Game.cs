using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Game
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("photo")]
        public List<PhotoSize> PhotoSizeList { get; set; } = new List<PhotoSize>();
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("text_entities")]
        public List<MessageEntity> TextEntityList { get; set; }
        [JsonProperty("animation")]
        public Animation Animation { get; set; }
    }
}
