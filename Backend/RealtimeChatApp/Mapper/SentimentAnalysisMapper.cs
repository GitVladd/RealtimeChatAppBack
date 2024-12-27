using RealtimeChatApp.Dtos;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Mapper
{
    public static class SentimentAnalysisMapper
    {
        public static SentimentAnalysisGetDto ToSentimentAnalysisGetDto(this SentimentAnalysis sentimentAnalysis)
        {
            return new SentimentAnalysisGetDto
            {
                Id = sentimentAnalysis.Id,
                Sentiment = sentimentAnalysis.Sentiment,
                ConfidenceScoresPositive = sentimentAnalysis.ConfidenceScoresPositive,
                ConfidenceScoresNegative = sentimentAnalysis.ConfidenceScoresNegative,
                ConfidenceScoresNeutral = sentimentAnalysis.ConfidenceScoresNeutral,
                AnalyzedAt = sentimentAnalysis.AnalyzedAt
            };
        }
    }
}
