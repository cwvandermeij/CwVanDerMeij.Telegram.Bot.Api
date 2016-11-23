using CwVanDerMeij.Telegram.Bot.Api.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwVanDerMeij.Telegram.Bot.Api.Models
{
    public class Message
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
        [JsonProperty("from")]
        public User From { get; set; }
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixTimeStampDateTimeConverter))]
        public DateTime Date { get; set; }
        [JsonProperty("chat")]
        public Chat Chat { get; set; }
        [JsonProperty("forward_from")]
        public User FordwardFrom { get; set; }
        [JsonProperty("forward_from_chat")]
        public Chat ForwardFromChat { get; set; }
        [JsonProperty("forward_date")]
        public DateTime? ForwardDate { get; set; }
        [JsonProperty("reply_to_message")]
        public Message ReplyToMessage { get; set; }
        [JsonProperty("edit_date")]
        public DateTime? EditDate { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("entities")]
        public List<MessageEntity> MessageEntityList { get; set; }
        [JsonProperty("audio")]
        public Audio Audio { get; set; }
        [JsonProperty("document")]
        public Document Document { get; set; }
        [JsonProperty("game")]
        public Game Game { get; set; }
        [JsonProperty("photo")]
        public List<PhotoSize> PhotoSizeList { get; set; } = new List<PhotoSize>();
        [JsonProperty("sticker")]
        public Sticker Sticker { get; set; }
        [JsonProperty("video")]
        public Video Video { get; set; }
        [JsonProperty("voice")]
        public Voice Voice { get; set; }
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("venue")]
        public Venue Venue { get; set; }
        [JsonProperty("new_chat_member")]
        public User NewChatMember { get; set; }
        [JsonProperty("left_chat_member")]
        public User LeftChatMember { get; set; }
        [JsonProperty("new_chat_title")]
        public string NewChatTitle { get; set; }
        [JsonProperty("new_chat_photo")]
        public List<PhotoSize> NewChatPhotoSizeList { get; set; } = new List<PhotoSize>();
        [JsonProperty("delete_chat_photo")]
        public bool? DeleteChatPhoto { get; set; }
        [JsonProperty("group_chat_created")]
        public bool? GroupChatCreated { get; set; }
        [JsonProperty("supergroup_chat_created")]
        public bool? SupergroupChatCreated { get; set; }
        [JsonProperty("channel_chat_created")]
        public bool? ChannelChatCreated { get; set; }
        [JsonProperty("migrate_to_chat_id")]
        public long? MigrateToChatId { get; set; }
        [JsonProperty("migrate_from_chat_id")]
        public long? MigrateFromChatId { get; set; }
        [JsonProperty("pinned_message")]
        public Message PinnedMessage { get; set; }
    }
}
