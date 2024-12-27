using RealtimeChatApp.Models;

namespace RealtimeChatApp.Services.Cache
{
    public interface IChatMessageCache
    {
        int Capacity { get; }

        void Add(ChatMessage message);

        void AddRange(IEnumerable<ChatMessage> chatMessages);

        IReadOnlyCollection<ChatMessage> Get(int take = int.MaxValue, int skip = 0);

        public void Initialize(IEnumerable<ChatMessage> messages);
    }
}
