using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode
{
    public class InlineKeyboardMarkup : IKeyboardMarkup
    {
        [JsonProperty("inline_keyboard")]
        public List<List<InlineKeyboardButton>> InlineKeyboardButtonListList { get; set; } = new List<List<InlineKeyboardButton>>();
    }
}
