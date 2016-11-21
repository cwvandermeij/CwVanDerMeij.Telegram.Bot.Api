using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class ApiResult<T>
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
