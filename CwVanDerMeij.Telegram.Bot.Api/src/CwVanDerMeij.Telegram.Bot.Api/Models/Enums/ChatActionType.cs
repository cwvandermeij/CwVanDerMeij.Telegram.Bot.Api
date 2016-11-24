using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models.Enums
{
    public enum ChatActionType
    {
        typing,
        upload_photo,
        record_video,
        upload_video,
        record_audio,
        upload_audio,
        upload_document,
        find_location
    }
}
