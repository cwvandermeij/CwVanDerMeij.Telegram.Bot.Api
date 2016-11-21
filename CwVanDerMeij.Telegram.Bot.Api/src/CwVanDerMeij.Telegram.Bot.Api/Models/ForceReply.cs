using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class ForceReply : IKeyboardMarkup
    {
        [JsonProperty("force_reply")]
        public bool ForceAReply { get; set; }
        [JsonProperty("selective")]
        public bool Selective { get; set; }
    }
}
