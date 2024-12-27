using Microsoft.AspNetCore.Http.HttpResults;
using RealtimeChatApp.Dtos;
using RealtimeChatApp.Models;

namespace RealtimeChatApp.Mapper
{
    public static class ChatMessageMapper
    {
        public static ChatMessageGetDto ToChatMessageGetDto(this ChatMessage chatMessage) 
        {
            return new ChatMessageGetDto
            {
                Id = chatMessage.Id,
                User = chatMessage.User,
                Message = chatMessage.Message,
                SentimentAnalysis = chatMessage.SentimentAnalysis?.ToSentimentAnalysisGetDto(),
                Timestamp = chatMessage.Timestamp
            };
        }

        public static ChatMessage ToChatMessage(this ChatMessageCreateDto createDto)
        {
            return new ChatMessage
            {
                Id = Guid.NewGuid(),
                User = createDto.User,
                Message = createDto.Message,
                SentimentAnalysisId = Guid.Empty,
                SentimentAnalysis = null,
                Timestamp = createDto.Timestamp
            };
        }
    }
}
