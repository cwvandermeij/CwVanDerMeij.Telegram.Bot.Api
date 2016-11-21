using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Voice
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        [JsonProperty("file_size")]
        public int? FileSize { get; set; }
    }
}
