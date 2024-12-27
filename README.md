# Realtime Chat Application Backend

A real-time chat application backend built with ASP.NET Core, SignalR, and Azure services. The application provides real-time messaging capabilities with sentiment analysis for messages.

Link on UI Project: https://github.com/GitVladd/RealtimeChatAppUI

### Features

- Real-time messaging using SignalR
- Message sentiment analysis using Azure Cognitive Services
- Message persistence using Entity Framework Core
- RESTful API endpoints for message retrieval
- Global exception handling
- Pagination support for message history

### Technology Stack

- ASP.NET Core 8.0
- SignalR - For real-time communication
- Entity Framework Core - For database operations
- Azure Cognitive Services - For sentiment analysis
- Azure Key Vault - For secrets management
- Azure SignalR Service - For scalable real-time messaging

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- Azure subscription (for Azure services)
- SQL Server (local or Azure SQL Database)

### Configuration

1. Clone the repository
2. Create appsettings.Development.json with the following structure:

{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedOrigins": {
        "Frontend": "http://localhost:4201" //your frontend url
    }
}

3. Configure Azure Key Vault URL in appsettings.json
4. Set up required Azure services:
   - Azure Key Vault
   - Azure Cognitive Services
   - Azure SignalR Service (optional)
5. Edit KeyVaultUrl and AllowedOrigins in appsettings.json

### Getting Started

1. Restore NuGet packages:
dotnet restore

2. Update database:
dotnet ef database update

3. Run the application:
dotnet run
