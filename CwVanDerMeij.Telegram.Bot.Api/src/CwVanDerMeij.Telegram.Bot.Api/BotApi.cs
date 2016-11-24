using CwVanDerMeij.Telegram.Bot.Api.Models;
using CwVanDerMeij.Telegram.Bot.Api.Models.Enums;
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
    public class BotApi
    {
        private const string API_URL = "https://api.telegram.org/bot";
        private readonly string msBotToken;
        private HttpClient moHttpClient = new HttpClient();
        private JsonSerializer moJsonSerializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };

        public BotApi(string psBotToken)
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

        public async Task<Message> SendMessage(long pnChatId, string psText, string psParseMode, bool? pbDisableWebPagePreview, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
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

        public async Task<Message> ForwardMessage(long pnChatId, int pnFromChatId, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), FromChatIdOrChannelUsername = pnFromChatId.ToString(), DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }

        public async Task<Message> ForwardMessage(string psChannelUsername, int pnFromChatId, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = psChannelUsername, FromChatIdOrChannelUsername = pnFromChatId.ToString(), DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }

        public async Task<Message> ForwardMessage(long pnChatId, string psFromChannelUsername, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), FromChatIdOrChannelUsername = psFromChannelUsername, DisableNotification = pbDisableNotification, MessageId = pnMessageId });
        }

        public async Task<Message> ForwardMessage(string psChannelUsername, string psFromChannelUsername, bool? pbDisableNotification, int pnMessageId)
        {
            return await ForwardMessage(new ForwardMessageParameters() { ChatIdOrChannelUsername = psChannelUsername, FromChatIdOrChannelUsername = psFromChannelUsername, DisableNotification = pbDisableNotification, MessageId = pnMessageId });
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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
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

        public async Task<Message> SendPhoto(long pnChatId, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(string psChannelUsername, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(long pnChatId, string psPhotoIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), PhotoIdOrPhotoUrl = psPhotoIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendPhoto(string psChannelUsername, string psPhotoIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendPhoto(new SendPhotoParameters() { ChatIdOrChannelUsername = psChannelUsername, PhotoIdOrPhotoUrl = psPhotoIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }
    
        private async Task<Message> SendAudio(SendAudioParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "audio", p.InputFile.FileName);

                if (p.Caption != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Caption), "caption");
                }

                if (p.Duration.HasValue)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Duration.ToString()), "duration");
                }

                if (p.Performer != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Performer), "performer");
                }

                if (p.Title != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Title), "title");
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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendAudio", loHttpContent);
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

        public async Task<Message> SendAudio(long pnChatId, InputFile poInputFile, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendAudio(new SendAudioParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, Performer = psPerformer, Title = psTitle, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendAudio(string psChannelUsername, InputFile poInputFile, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendAudio(new SendAudioParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, Performer = psPerformer, Title = psTitle, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendAudio(long pnChatId, string psAudioIdOrUrl, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendAudio(new SendAudioParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), AudioIdOrAudioUrl = psAudioIdOrUrl, Caption = psCaption, Duration = pnDuration, Performer = psPerformer, Title = psTitle, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendAudio(string psChannelUsername, string psAudioIdOrUrl, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendAudio(new SendAudioParameters() { ChatIdOrChannelUsername = psChannelUsername, AudioIdOrAudioUrl = psAudioIdOrUrl, Caption = psCaption, Duration = pnDuration, Performer = psPerformer, Title = psTitle, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendDocument(SendDocumentParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "document", p.InputFile.FileName);

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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendDocument", loHttpContent);
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

        public async Task<Message> SendDocument(long pnChatId, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendDocument(new SendDocumentParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendDocument(string psChannelUsername, InputFile poInputFile, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendDocument(new SendDocumentParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendDocument(long pnChatId, string psDocumentIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendDocument(new SendDocumentParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), DocumentIdOrDocumentUrl = psDocumentIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendDocument(string psChannelUsername, string psDocumentIdOrUrl, string psCaption, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendDocument(new SendDocumentParameters() { ChatIdOrChannelUsername = psChannelUsername, DocumentIdOrDocumentUrl = psDocumentIdOrUrl, Caption = psCaption, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendSticker(SendStickerParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "sticker", p.InputFile.FileName);
                
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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendSticker", loHttpContent);
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

        public async Task<Message> SendSticker(long pnChatId, InputFile poInputFile, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendSticker(new SendStickerParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendSticker(string psChannelUsername, InputFile poInputFile, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendSticker(new SendStickerParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendSticker(long pnChatId, string psStickerIdOrUrl, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendSticker(new SendStickerParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), StickerIdOrStickerUrl = psStickerIdOrUrl, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendSticker(string psChannelUsername, string psStickerIdOrUrl, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendSticker(new SendStickerParameters() { ChatIdOrChannelUsername = psChannelUsername, StickerIdOrStickerUrl = psStickerIdOrUrl, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendVideo(SendVideoParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "video", p.InputFile.FileName);

                if (p.Duration.HasValue)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Duration.ToString()), "duration");
                }

                if (p.Width != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Width.ToString()), "width");
                }

                if (p.Height != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Height.ToString()), "height");
                }

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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendVideo", loHttpContent);
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

        public async Task<Message> SendVideo(long pnChatId, InputFile poInputFile, string psCaption, int? pnDuration, int? pnWidth, int? pnHeight, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVideo(new SendVideoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, Width = pnWidth, Height = pnHeight, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVideo(string psChannelUsername, InputFile poInputFile, string psCaption, int? pnDuration, int? pnWidth, int? pnHeight, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVideo(new SendVideoParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, Width = pnWidth, Height = pnHeight, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVideo(long pnChatId, string psVideoIdOrUrl, string psCaption, int? pnDuration, int? pnWidth, int? pnHeight, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVideo(new SendVideoParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), VideoIdOrVideoUrl = psVideoIdOrUrl, Caption = psCaption, Duration = pnDuration, Width = pnWidth, Height = pnHeight, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVideo(string psChannelUsername, string psVideoIdOrUrl, string psCaption, int? pnDuration, int? pnWidth, int? pnHeight, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVideo(new SendVideoParameters() { ChatIdOrChannelUsername = psChannelUsername, VideoIdOrVideoUrl = psVideoIdOrUrl, Caption = psCaption, Duration = pnDuration, Width = pnWidth, Height = pnHeight, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendVoice(SendVoiceParameters p)
        {
            ApiResult<Message> loResult;

            HttpContent loHttpContent;

            if (p.InputFile != null)
            {
                MultipartFormDataContent loMultipartFormDataContent = new MultipartFormDataContent();
                loMultipartFormDataContent.Add(new StringContent(p.ChatIdOrChannelUsername), "chat_id");
                loMultipartFormDataContent.Add(new ByteArrayContent(p.InputFile.FileData), "voice", p.InputFile.FileName);

                if (p.Caption != null)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Caption), "caption");
                }

                if (p.Duration.HasValue)
                {
                    loMultipartFormDataContent.Add(new StringContent(p.Duration.ToString()), "duration");
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
                    loMultipartFormDataContent.Add(new StringContent(JsonSerialize(p.ReplyMarkup)), "reply_markup");
                }

                loHttpContent = loMultipartFormDataContent;
            }
            else
            {
                loHttpContent = new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json");
            }

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendVoice", loHttpContent);
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

        public async Task<Message> SendVoice(long pnChatId, InputFile poInputFile, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVoice(new SendVoiceParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVoice(string psChannelUsername, InputFile poInputFile, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVoice(new SendVoiceParameters() { ChatIdOrChannelUsername = psChannelUsername, InputFile = poInputFile, Caption = psCaption, Duration = pnDuration, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVoice(long pnChatId, string psVoiceIdOrUrl, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVoice(new SendVoiceParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), VoiceIdOrVoiceUrl = psVoiceIdOrUrl, Caption = psCaption, Duration = pnDuration, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVoice(string psChannelUsername, string psVoiceIdOrUrl, string psCaption, int? pnDuration, string psPerformer, string psTitle, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVoice(new SendVoiceParameters() { ChatIdOrChannelUsername = psChannelUsername, VoiceIdOrVoiceUrl = psVoiceIdOrUrl, Caption = psCaption, Duration = pnDuration, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendLocation(SendLocationParameters p)
        {
            ApiResult<Message> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendLocation", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
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

        public async Task<Message> SendLocation(long pnChatId, float pnLatitude, float pnLongitude, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendLocation(new SendLocationParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), Latitude = pnLatitude, Longitude = pnLongitude, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendLocation(string psChannelUsername, float pnLatitude, float pnLongitude, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendLocation(new SendLocationParameters() { ChatIdOrChannelUsername = psChannelUsername, Latitude = pnLatitude, Longitude = pnLongitude, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendVenue(SendVenueParameters p)
        {
            ApiResult<Message> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendVenue", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
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

        public async Task<Message> SendVenue(long pnChatId, float pnLatitude, float pnLongitude, string psTitle, string psAddress, string psFoursquareId, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVenue(new SendVenueParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), Latitude = pnLatitude, Longitude = pnLongitude, Title = psTitle, Address = psAddress, FoursquareId = psFoursquareId, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendVenue(string psChannelUsername, float pnLatitude, float pnLongitude, string psTitle, string psAddress, string psFoursquareId, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendVenue(new SendVenueParameters() { ChatIdOrChannelUsername = psChannelUsername, Latitude = pnLatitude, Longitude = pnLongitude, Title = psTitle, Address = psAddress, FoursquareId = psFoursquareId, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<Message> SendContact(SendContactParameters p)
        {
            ApiResult<Message> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendContact", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
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

        public async Task<Message> SendContact(long pnChatId, string psPhoneNumber, string psFirstName, string psLastName, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendContact(new SendContactParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), PhoneNumber = psPhoneNumber, FirstName = psFirstName, LastName = psLastName, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        public async Task<Message> SendContact(string psChannelUsername, string psPhoneNumber, string psFirstName, string psLastName, bool? pbDisableNotification, int? pnReplyToMessageId, IKeyboardMarkup poReplyMarkup)
        {
            return await SendContact(new SendContactParameters() { ChatIdOrChannelUsername = psChannelUsername, PhoneNumber = psPhoneNumber, FirstName = psFirstName, LastName = psLastName, DisableNotification = pbDisableNotification, ReplyToMessageId = pnReplyToMessageId, ReplyMarkup = poReplyMarkup });
        }

        private async Task<bool> SendChatAction(SendChatActionParameters p)
        {
            ApiResult<bool> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("sendChatAction", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<bool>>(lsContent);
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

        public async Task<bool> SendChatAction(long pnChatId, ChatActionType poAction)
        {
            return await SendChatAction(new SendChatActionParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), Action = poAction});
        }

        public async Task<bool> SendChatAction(string psChannelUsername, ChatActionType poAction)
        {
            return await SendChatAction(new SendChatActionParameters() { ChatIdOrChannelUsername = psChannelUsername, Action = poAction });
        }

        private async Task<UserProfilePhotos> GetUserProfilePhotos(GetUserProfilePhotosParameters p)
        {
            ApiResult<UserProfilePhotos> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getUserProfilePhotos", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<UserProfilePhotos>>(lsContent);
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

        public async Task<UserProfilePhotos> GetUserProfilePhotos(int pnUserId, int? pnOffset, int? pnLimit)
        {
            return await GetUserProfilePhotos(new GetUserProfilePhotosParameters() { UserId = pnUserId, Offset = pnOffset, Limit = pnLimit });
        }

        private async Task<Models.File> GetFile(GetFileParameters p)
        {
            ApiResult<Models.File> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getFile", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<Models.File>>(lsContent);
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

        public async Task<Models.File> GetFile(string psFileId)
        {
            return await GetFile(new GetFileParameters() { FileId = psFileId });
        }

        private async Task<bool> KickChatMember(KickChatMemberParameters p)
        {
            ApiResult<bool> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("kickChatMember", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<bool>>(lsContent);
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

        public async Task<bool> KickChatMember(long pnChatId, int pnUserId)
        {
            return await KickChatMember(new KickChatMemberParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), UserId = pnUserId });
        }

        public async Task<bool> KickChatMember(string psChannelUsername, int pnUserId)
        {
            return await KickChatMember(new KickChatMemberParameters() { ChatIdOrChannelUsername = psChannelUsername, UserId = pnUserId });
        }

        private async Task<bool> LeaveChat(LeaveChatParameters p)
        {
            ApiResult<bool> loResult;
            
            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("leaveChat", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<bool>>(lsContent);
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

        public async Task<bool> LeaveChat(long pnChatId)
        {
            return await LeaveChat(new LeaveChatParameters() { ChatIdOrChannelUsername = pnChatId.ToString() });
        }

        public async Task<bool> LeaveChat(string psChannelUsername)
        {
            return await LeaveChat(new LeaveChatParameters() { ChatIdOrChannelUsername = psChannelUsername });
        }

        private async Task<bool> UnbanChatMember(UnbanChatMemberParameters p)
        {
            ApiResult<bool> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("unbanChatMember", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<bool>>(lsContent);
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

        public async Task<bool> UnbanChatMember(long pnChatId, int pnUserId)
        {
            return await UnbanChatMember(new UnbanChatMemberParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), UserId = pnUserId });
        }

        public async Task<bool> UnbanChatMember(string psChannelUsername, int pnUserId)
        {
            return await UnbanChatMember(new UnbanChatMemberParameters() { ChatIdOrChannelUsername = psChannelUsername, UserId = pnUserId });
        }

        private async Task<Chat> GetChat(GetChatParameters p)
        {
            ApiResult<Chat> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getChat", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<Chat>>(lsContent);
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

        public async Task<Chat> GetChat(long pnChatId)
        {
            return await GetChat(new GetChatParameters() { ChatIdOrChannelUsername = pnChatId.ToString() });
        }

        public async Task<Chat> GetChat(string psChannelUsername)
        {
            return await GetChat(new GetChatParameters() { ChatIdOrChannelUsername = psChannelUsername });
        }

        private async Task<List<ChatMember>> GetChatAdministrators(GetChatAdministratorsParameters p)
        {
            ApiResult<List<ChatMember>> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getChatAdministrators", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<List<ChatMember>>>(lsContent);
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

        public async Task<List<ChatMember>> GetChatAdministrators(long pnChatId)
        {
            return await GetChatAdministrators(new GetChatAdministratorsParameters() { ChatIdOrChannelUsername = pnChatId.ToString() });
        }

        public async Task<List<ChatMember>> GetChatAdministrators(string psChannelUsername)
        {
            return await GetChatAdministrators(new GetChatAdministratorsParameters() { ChatIdOrChannelUsername = psChannelUsername });
        }

        private async Task<int> GetChatMembersCount(GetChatMembersCountParameters p)
        {
            ApiResult<int> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getChatMembersCount", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<int>>(lsContent);
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

        public async Task<int> GetChatMembersCount(long pnChatId)
        {
            return await GetChatMembersCount(new GetChatMembersCountParameters() { ChatIdOrChannelUsername = pnChatId.ToString() });
        }

        public async Task<int> GetChatMembersCount(string psChannelUsername)
        {
            return await GetChatMembersCount(new GetChatMembersCountParameters() { ChatIdOrChannelUsername = psChannelUsername });
        }

        private async Task<ChatMember> GetChatMember(GetChatMemberParameters p)
        {
            ApiResult<ChatMember> loResult;

            try
            {
                HttpResponseMessage loHttpResponseMessage = await moHttpClient.PostAsync("getChatMember", new StringContent(JsonSerialize(p), Encoding.UTF8, "application/json"));
                string lsContent = await loHttpResponseMessage.Content.ReadAsStringAsync();

                loResult = JsonDeserialize<ApiResult<ChatMember>>(lsContent);
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

        public async Task<ChatMember> GetChatMember(long pnChatId, int pnUserId)
        {
            return await GetChatMember(new GetChatMemberParameters() { ChatIdOrChannelUsername = pnChatId.ToString(), UserId = pnUserId });
        }

        public async Task<ChatMember> GetChatMember(string psChannelUsername, int pnUserId)
        {
            return await GetChatMember(new GetChatMemberParameters() { ChatIdOrChannelUsername = psChannelUsername, UserId = pnUserId });
        }
    }
}
