using CwVanDerMeij.Telegram.Bot.Api.Models;
using CwVanDerMeij.Telegram.Bot.Api.Models.InlineMode;
using CwVanDerMeij.Telegram.Bot.Api.Models.Interfaces;
using CwVanDerMeij.Telegram.Bot.Api.Parameters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api
{
    public class Bot
    {
        private const string API_URL = "https://api.telegram.org/bot";
        private readonly string msBotToken;
        private HttpClient moHttpClient = new HttpClient();
        private JsonSerializer moJsonSerializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };

        public Bot(string psBotToken)
        {
            msBotToken = psBotToken;
            moHttpClient.BaseAddress = new Uri(API_URL + msBotToken + "/");
        }

        private string JsonSerialize<T>(T poObject)
        {
            using (StringWriter loStringWriter = new StringWriter())
            {
                moJsonSerializer.Serialize(loStringWriter, poObject);

                return loStringWriter.ToString();
            }
        }

        private T JsonDeserialize<T>(string psJson)
        {
            using (StringReader loStringReader = new StringReader(psJson))
            {
                using (JsonTextReader loJsonTextReader = new JsonTextReader(loStringReader))
                {
                    return moJsonSerializer.Deserialize<T>(loJsonTextReader);
                }
            }
        }
        
        public async Task<User> GetMe()
        {
            ApiResult<User> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.GetAsync("getMe");
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<User>>(lsContent);
            }
            catch (Exception loException)
            {
                throw loException;
            }

            if (loResult.Ok)
            {
                return loResult.Result;
            }
            else
            {
                throw new Exception(loResult.Description);
            }
        }

        private async Task<Message> SendMessage(SendMessageParameters p)
        {
            ApiResult<Message> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendMessage", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();
                
                loResult = JsonDeserialize<ApiResult<Message>>(lsContent);
            }
            catch (Exception loException)
            {
                throw loException;
            }

            if (loResult.Ok)
            {
                return loResult.Result;
            }
            else
            {
                throw new Exception(loResult.Description);
            }
        }

        public async Task<Message> SendMessage(int pnChatId, string psText, string psParseMode, bool? pbDisableWebPagePreview, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendMessage(new SendMessageParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), Text = psText, ParseMode = psParseMode, DisableWebPagePreview = pbDisableWebPagePreview, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }
        
        public async Task<Message> SendMessage(string psChannelUsername, string psText, string psParseMode, bool? pbDisableWebPagePreview, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendMessage(new SendMessageParameters() { ChatIdOrChannelUsername = psChannelUsername, Text = psText, ParseMode = psParseMode, DisableWebPagePreview = pbDisableWebPagePreview, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> ForwardMessage(ForwardMessageParameters p)
        {
            ApiResult<Message> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("forwardMessage", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<Message>>(lsContent);
            }
            catch (Exception loException)
            {
                throw loException;
            }

            if (loResult.Ok)
            {
                return loResult.Result;
            }
            else
            {
                throw new Exception(loResult.Description);
            }
        }

        public async Task<Message> ForwardMessage(int pnChatId, int pnFromChatId, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), FromChatId = pnFromChatId.ToString(), DisableNotification = pbDisableNotification, MessageId = pnMessageId});
        }

        public async Task<Message> ForwardMessage(string psChannelUsername, int pnFromChatId, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = psChannelUsername, FromChatId = pnFromChatId.ToString(), DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }

        public async Task<Message> ForwardMessage(int pnChatId, string psFromChannelUsername, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), FromChatId = psFromChannelUsername, DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }

        public async Task<Message> ForwardMessage(string psChannelUsername, string psFromChannelUsername, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = psChannelUsername, FromChatId = psFromChannelUsername, DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }
        
        private async Task<Message> SendPhoto(SendPhotoParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "photo", p.InputFile.FileName);

                if (p.Caption != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Caption), "caption");
                }

                if (p.DisableNotification.HasValue)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.DisableNotification.ToString()), "disable_notification");
                }

                if (p.ReplyToMessageId.HasValue)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.ReplyToMessageId.ToString()), "reply_to_message_id");
                }

                if (p.ReplyMarkup != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_to_message_id");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendPhoto", loHttpContent);
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<Message>>(lsContent);
            }
            catch (Exception loException)
            {
                throw loException;
            }

            if (loResult.Ok)
            {
                return loResult.Result;
            }
            else
            {
                throw new Exception(loResult.Description);
            }
        }

        public async Task<Message> SendPhoto(int pnChatId, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(string psChannelUsername, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(int pnChatId, string psPhotoIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), PhotoIdOrPhotoUrl = psPhotoIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(string psChannelUsername, string psPhotoIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = psChannelUsername, PhotoIdOrPhotoUrl = psPhotoIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }
    }
}
