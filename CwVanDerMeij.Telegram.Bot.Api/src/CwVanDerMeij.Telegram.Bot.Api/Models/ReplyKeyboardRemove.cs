using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class ReplyKeyboardRemove : IKeyboardMarkup
    {
        [JsonProperty("hide_keyboard")]
        public bool HideKeyboard { get; set; }
        [JsonProperty("selective")]
        public bool Selective { get; set; }
    }
}
