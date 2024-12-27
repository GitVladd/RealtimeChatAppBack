using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.Data;
using RealtimeChatApp.Models;
using RealtimeChatApp.Repository.Base;
namespace RealtimeChatApp.Repository
{
    public class ChatMessageRepository : BaseRepository<Guid, ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(ApplicationDbContext context) : base(context){}
        public override async Task<IEnumerable<ChatMessage>> GetAsync(int take = int.MaxValue, int skip = 0, CancellationToken cancellationToken = default)
        {
            if (take < 0) throw new ArgumentException("Invalid take parameter: must be non-negative.");
            if (skip < 0) throw new ArgumentException("Invalid skip parameter: must be non-negative.");

            return await Entities.OrderByDescending(cm => cm.Timestamp).Skip(skip).Take(take).Include(cm=> cm.SentimentAnalysis).ToListAsync(cancellationToken);
        }
    }
}
