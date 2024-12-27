using RealtimeChatApp.Dtos;
using RealtimeChatApp.Models;
using RealtimeChatApp.QueryParams;

namespace RealtimeChatApp.Services
{
    public interface IChatMessageService
    {
        Task<ChatMessage> AddMessageAsync(ChatMessageCreateDto createDto, SentimentAnalysis? sentimentAnalysis = null);
        Task<IEnumerable<ChatMessageGetDto>> GetMessagesAsync(PaginationParameter pagination, CancellationToken cancellationToken = default);
    }
}
