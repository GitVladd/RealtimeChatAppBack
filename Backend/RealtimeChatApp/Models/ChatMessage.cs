using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.Models
{
    public class ChatMessage : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }
        
        public Guid SentimentAnalysisId { get; set; }

        [ForeignKey(nameof(SentimentAnalysisId))]
        public SentimentAnalysis SentimentAnalysis { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
