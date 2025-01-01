using System.Text.Json.Serialization;

namespace Mde.Project.Mobile.Models
{
    public class NotificationMessageBody
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
