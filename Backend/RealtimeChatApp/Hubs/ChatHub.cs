using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.SignalR;
using RealtimeChatApp.Dtos;
using RealtimeChatApp.Mapper;
using RealtimeChatApp.Models;
using RealtimeChatApp.Services;

namespace RealtimeChatApp.Hubs
{
    public class ChatHub : Hub
    {

        private readonly ISentimentAnalysisService _sentimentService;
        private readonly IChatMessageService _chatMessageService;

        public ChatHub(ISentimentAnalysisService sentimentService, IChatMessageService chatMessageService)
        {
            _sentimentService = sentimentService;
            _chatMessageService = chatMessageService;
        }

        public async Task SendMessage(ChatMessageCreateDto messageCreateDto)
        {
            SentimentAnalysis sentimentAnalysisEntity = await _sentimentService.GenerateSentimentAnalysisAsync(messageCreateDto.Message);

            ChatMessage chatMessageEntity = await _chatMessageService.AddMessageAsync(messageCreateDto, sentimentAnalysisEntity);
            sentimentAnalysisEntity.ChatMessageId = chatMessageEntity.Id;

            await _sentimentService.AddSentimentAnalysisAsync(sentimentAnalysisEntity);
          
            ChatMessageGetDto responseChatMessage = chatMessageEntity.ToChatMessageGetDto();
            

            await Clients.All.SendAsync("ReceiveMessage", responseChatMessage);
        }
    }
}
