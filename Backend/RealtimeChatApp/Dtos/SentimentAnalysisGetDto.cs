namespace RealtimeChatApp.Dtos
{
    public class SentimentAnalysisGetDto
    {
        public Guid Id { get; set; }
        public string Sentiment { get; set; }

        public double ConfidenceScoresPositive { get; set; }

        public double ConfidenceScoresNegative { get; set; }

        public double ConfidenceScoresNeutral { get; set; }

        public DateTime AnalyzedAt { get; set; }
    }
}
