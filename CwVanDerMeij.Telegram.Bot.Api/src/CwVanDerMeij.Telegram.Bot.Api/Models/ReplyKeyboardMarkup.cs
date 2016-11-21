using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class ReplyKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("keyboard")]
        public List<List<KeyboardButton>> KeyboardButtonListList { get; set; } = new List<List<KeyboardButton>>();
        [JsonProperty("resize_keyboard")]
        public bool? ResizeKeyboard { get; set; }
        [JsonProperty("one_time_keyboard")]
        public bool? OneTimeKeyboard { get; set; }
        [JsonProperty("selective")]
        public bool? Selective { get; set; }
    }
}
