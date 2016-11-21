using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class InputFile
    {
        public InputFile(string psFileName, byte[] paFileByteArray)
        {
            FileName = psFileName;
            FileData = paFileByteArray;
        }
        
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}
