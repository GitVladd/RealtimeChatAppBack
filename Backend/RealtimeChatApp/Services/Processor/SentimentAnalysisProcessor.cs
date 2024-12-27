using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Services.Processor
{
    public class SentimentAnalysisProcessor : ISentimentAnalysisProcessor
    {
        private readonly TextAnalyticsClient _client;

        public SentimentAnalysisProcessor(TextAnalyticsClient client)
        {
            _client = client;
        }

        public async Task<DocumentSentiment> AnalyzeSentimentAsync(string message)
        {
            return await _client.AnalyzeSentimentAsync(message);
        }
    }
}
