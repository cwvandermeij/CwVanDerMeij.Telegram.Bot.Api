using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class File
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }
        [JsonProperty("file_size")]
        public int? FileSize { get; set; }
        [JsonProperty("file_path")]
        public string FilePath { get; set; }
    }
}
