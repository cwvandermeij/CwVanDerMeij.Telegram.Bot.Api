using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class UserProfilePhotos
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("photos")]
        public List<List<PhotoSize>> PhotoSizeListList { get; set; } = new List<List<PhotoSize>>();
    }
}
