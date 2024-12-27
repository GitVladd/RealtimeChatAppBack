using RealtimeChatApp.Data;
using RealtimeChatApp.Models;
using RealtimeChatApp.Repository.Base;

namespace RealtimeChatApp.Repository
{
    public class SentimentAnalysisRepository : BaseRepository<Guid, SentimentAnalysis>, ISentimentAnalysisRepository
    {
        public SentimentAnalysisRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
