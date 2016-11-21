using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.Enums
{
    public enum MessageEntityType
    {
        mention,
        hashtag,
        bot_command,
        url,
        email,
        bold,
        italic,
        code,
        pre,
        text_link,
        text_mention
    }
}
