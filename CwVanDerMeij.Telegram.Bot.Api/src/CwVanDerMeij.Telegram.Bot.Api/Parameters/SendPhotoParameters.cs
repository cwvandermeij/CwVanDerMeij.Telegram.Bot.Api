using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class SendPhotoParameters
    {
        [JsonProperty("chat_id")]
        public string ChatIdOrChannelUsername { get; set; }
        [JsonProperty("photo")]
        public string PhotoIdOrPhotoUrl { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("disable_notification")]
        public bool? DisableNotification { get; set; }
        [JsonProperty("reply_to_message_id")]
        public int? ReplyToMessageId { get; set; }
        [JsonProperty("reply_markup")]
        public IKeyboardMarkup ReplyMarkup { get; set; }

        [JsonIgnore]
        public InputFile InputFile { get; set; }
    }
}
