using System.Text.Json.Serialization;

namespace Mde.Project.Mobile.Models
{
    public class MessageBody
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("notification")]
        public NotificationMessageBody Notification { get; set; }
    }
}
