using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Models
{
    public class SentimentAnalysis : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ChatMessageId { get; set; }

        [ForeignKey(nameof(ChatMessageId))]
        public ChatMessage ChatMessage { get; set; } = null!;

        [Required]
        public string Sentiment { get; set; }

        [Required]
        [Range(-1.0, 1.0)]
        public double ConfidenceScoresPositive { get; set; }

        [Required]
        [Range(-1.0, 1.0)]
        public double ConfidenceScoresNegative { get; set; }

        [Required]
        [Range(-1.0, 1.0)]
        public double ConfidenceScoresNeutral { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}