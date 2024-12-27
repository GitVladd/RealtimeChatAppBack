using RealtimeChatApp.Dtos;
using RealtimeChatApp.Mapper;
using RealtimeChatApp.Models;
using RealtimeChatApp.QueryParams;
using RealtimeChatApp.Repository;
using RealtimeChatApp.Services.Cache;

namespace RealtimeChatApp.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatMessageCache _chatMessageCache;

        public ChatMessageService(IChatMessageRepository chatMessageRepository, IChatMessageCache chatMessageCache)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatMessageCache = chatMessageCache;
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessageCreateDto createDto, SentimentAnalysis? sentimentAnalysis = null)
        {
            var entity = createDto.ToChatMessage();

            if(sentimentAnalysis != null)
            {
                entity.SentimentAnalysisId = sentimentAnalysis.Id;
            }

            await _chatMessageRepository.CreateAsync(entity);
            _chatMessageCache.Add(entity);

            return entity;
        }

        public async Task<IEnumerable<ChatMessageGetDto>> GetMessagesAsync(PaginationParameter pagination, CancellationToken cancellationToken = default)
        {
            int take = pagination.PageSize;
            int skip = (pagination.PageNumber - 1) * pagination.PageSize;
            
            var cachedMessages = _chatMessageCache.Get(take, skip);
            
            if (cachedMessages.Count == take)
            {
                return cachedMessages.Select(cm => cm.ToChatMessageGetDto());
            }

            var result = new List<ChatMessage>();
            
            result.AddRange(cachedMessages);
            
            int remainingCount = take - cachedMessages.Count;
            int additionalSkip = skip + cachedMessages.Count;
            
            var dbMessages = await _chatMessageRepository.GetAsync(remainingCount, additionalSkip, cancellationToken);
            result.AddRange(dbMessages);
            
            _chatMessageCache.AddRange(dbMessages);

            return result.Select(cm => cm.ToChatMessageGetDto());
        }
    }
}
