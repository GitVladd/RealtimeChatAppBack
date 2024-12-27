using Azure;
using Azure.AI.TextAnalytics;
using RealtimeChatApp.Models;
using RealtimeChatApp.Repository;
using RealtimeChatApp.Services.Processor;

namespace RealtimeChatApp.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly ISentimentAnalysisRepository _repository;

        private readonly ISentimentAnalysisProcessor _processor;

        public SentimentAnalysisService(ISentimentAnalysisProcessor processor, ISentimentAnalysisRepository repository)
        {
            _processor = processor;
            _repository = repository;
        }

        public async Task<SentimentAnalysis> AddSentimentAnalysisAsync(SentimentAnalysis sentimentAnalysis)
        {
            await _repository.CreateAsync(sentimentAnalysis);

            return sentimentAnalysis;
        }

        public async Task<SentimentAnalysis> GenerateSentimentAnalysisAsync(string message)
        {
            DocumentSentiment documentSentiment =  await _processor.AnalyzeSentimentAsync(message);

            var sentimentAnalysis = new SentimentAnalysis
            {
                Id = Guid.NewGuid(),
                Sentiment = documentSentiment.Sentiment.ToString(),
                ConfidenceScoresPositive = documentSentiment.ConfidenceScores.Positive,
                ConfidenceScoresNegative = documentSentiment.ConfidenceScores.Negative,
                ConfidenceScoresNeutral = documentSentiment.ConfidenceScores.Neutral,
                AnalyzedAt = DateTime.UtcNow
            };

            return sentimentAnalysis;
        }
    }
}
