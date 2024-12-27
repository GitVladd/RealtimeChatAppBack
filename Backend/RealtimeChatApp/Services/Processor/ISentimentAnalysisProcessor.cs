using Azure.AI.TextAnalytics;

namespace RealtimeChatApp.Services.Processor
{
    public interface ISentimentAnalysisProcessor
    {
        Task<DocumentSentiment> AnalyzeSentimentAsync(string message);
    }
}
