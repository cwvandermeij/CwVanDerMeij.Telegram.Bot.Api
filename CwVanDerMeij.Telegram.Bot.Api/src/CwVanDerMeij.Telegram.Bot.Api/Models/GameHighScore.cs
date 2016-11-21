using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class GameHighScore
    {
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
