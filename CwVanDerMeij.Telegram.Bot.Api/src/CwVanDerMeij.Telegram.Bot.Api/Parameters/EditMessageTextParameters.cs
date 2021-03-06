﻿using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Parameters
{
    public class EditMessageTextParameters
    {
        [JsonProperty("chat_id")]
        public string ChatIdOrChannelUsername { get; set; }
        [JsonProperty("message_id")]
        public int? MessageId { get; set; }
        [JsonProperty("inline_message_id")]
        public string InlineMessageId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("parse_mode")]
        public string ParseMode { get; set; }
        [JsonProperty("disable_web_page_preview")]
        public bool? DisableWebPagePreview { get; set; }
        [JsonProperty("reply_markup")]
        public IKeyboardMarkup ReplyMarkup { get; set; }
    }
}
