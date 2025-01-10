using System.Text.Json.Serialization;

namespace Mde.Project.Mobile.Models
{
    public class PushNotificationRequest
    {
        [JsonPropertyName("message")]
        public MessageBody Message { get; set; }
    }
}
