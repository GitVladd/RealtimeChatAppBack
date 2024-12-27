using RealtimeChatApp.Models;
using RealtimeChatApp.Repository.Base;

namespace RealtimeChatApp.Repository
{
    public interface IChatMessageRepository : IBaseRepository<Guid, ChatMessage>
    {
    }
}
