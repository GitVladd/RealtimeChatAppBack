using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealtimeChatApp.Dtos
{
    public class ChatMessageCreateDto
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }

        [JsonIgnore]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
