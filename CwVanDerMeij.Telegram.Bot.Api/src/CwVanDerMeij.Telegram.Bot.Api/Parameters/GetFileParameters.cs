using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class GetFileParameters
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }
    }
}
