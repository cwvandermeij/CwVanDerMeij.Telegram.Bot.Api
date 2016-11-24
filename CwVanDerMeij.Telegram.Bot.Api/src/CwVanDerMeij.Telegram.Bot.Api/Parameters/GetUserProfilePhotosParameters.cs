using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class GetUserProfilePhotosParameters
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("offset")]
        public int? Offset { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }
    }
}
