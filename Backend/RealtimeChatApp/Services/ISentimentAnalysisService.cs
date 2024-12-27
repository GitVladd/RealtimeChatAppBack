using Azure.AI.TextAnalytics;
using RealtimeChatApp.Dtos;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Services
{
    public interface ISentimentAnalysisService
    {
        Task<SentimentAnalysis> GenerateSentimentAnalysisAsync(string message);

        Task<SentimentAnalysis> AddSentimentAnalysisAsync(SentimentAnalysis sa);
    }
}
