using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Serialization
{
    public class UnixTimeStampDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type poObjectType)
        {
            return poObjectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader poJsonReader, Type poObjectType, object poExistingValue, JsonSerializer poJsonSerializer)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)poJsonReader.Value).UtcDateTime;
        }

        public override void WriteJson(JsonWriter poJsonWriter, object poValue, JsonSerializer poJsonSerializer)
        {
            poJsonWriter.WriteValue(new DateTimeOffset((DateTime)poValue).ToUnixTimeSeconds());
        }
    }
}
