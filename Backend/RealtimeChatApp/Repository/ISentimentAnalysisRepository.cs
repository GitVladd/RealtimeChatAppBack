using RealtimeChatApp.Models;
using RealtimeChatApp.Repository.Base;

namespace RealtimeChatApp.Repository
{
    public interface ISentimentAnalysisRepository : IBaseRepository<Guid, SentimentAnalysis>
    {
    }
}
