﻿using RealtimeChatApp.Models;

namespace RealtimeChatApp.Dtos
{
    public class ChatMessageGetDto
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public string Message { get; set; }

        public SentimentAnalysisGetDto SentimentAnalysis { get; set; }

        public DateTime Timestamp { get; set; }
    }
}