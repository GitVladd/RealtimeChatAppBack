using RealtimeChatApp.Models;
using RealtimeChatApp.Services.Cache;

namespace RealtimeChatApp.Cache
{
    public class ChatMessageCache : IChatMessageCache
    {
        private readonly SortedList<DateTime, ChatMessage> _cache;
        private readonly object _lock = new object();
        private int _capacity;

        public int Capacity
        {
            get => _capacity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Capacity must be greater than 0.");
                _capacity = value;
            }
        }

        public ChatMessageCache(int capacity = 100)
        {
            Capacity = capacity;
            _cache = new SortedList<DateTime, ChatMessage>();
        }

        public void Add(ChatMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            lock (_lock)
            {
                _cache[message.Timestamp] = message;

                while (_cache.Count > Capacity)
                {
                    _cache.RemoveAt(0);
                }
            }
        }

        public void AddRange(IEnumerable<ChatMessage> chatMessages)
        {
            if (chatMessages == null) throw new ArgumentNullException(nameof(chatMessages));

            lock (_lock)
            {
                foreach (var message in chatMessages)
                {
                    Add(message);
                }
            }
        }

        public IReadOnlyCollection<ChatMessage> Get(int take = int.MaxValue, int skip = 0)
        {
            if (take < 0) throw new ArgumentException("Invalid take parameter: must be non-negative.");
            if (skip < 0) throw new ArgumentException("Invalid skip parameter: must be non-negative.");

            lock (_lock)
            {
                return _cache.Values.Reverse().Skip(skip).Take(take).ToList().AsReadOnly();
            }
        }

        public void Initialize(IEnumerable<ChatMessage> messages)
        {
            if (messages == null) throw new ArgumentNullException(nameof(messages));

            lock (_lock)
            {
                _cache.Clear();
                
                var latestMessages = messages
                    .OrderByDescending(m => m.Timestamp)
                    .Take(Capacity)
                    .OrderBy(m => m.Timestamp);

                foreach (var message in latestMessages)
                {
                    _cache[message.Timestamp] = message;
                }
            }
        }
    }
}
