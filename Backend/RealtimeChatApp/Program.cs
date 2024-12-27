using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.EntityFrameworkCore;
using RealtimeChatApp.Data;
using RealtimeChatApp.Hubs;
using RealtimeChatApp.Repository;
using RealtimeChatApp.Services;
using RealtimeChatApp.Services.Processor;
using RealtimeChatApp.Middlewares;
using RealtimeChatApp.Services.Cache;
using RealtimeChatApp.Cache;

namespace RealtimeChatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);

            if (app.Environment.IsDevelopment())
            {
                CheckDbConnection(app);
            }

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", builder =>
                {
                    builder
                        .WithOrigins(configuration["AllowedOrigins:Frontend"])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });


            ConfigureDatabaseService(services, configuration);
            ConfigureSignalIRService(services, configuration);
            ConfigureTextAnalyticsClient(services, configuration);

            services.AddSingleton<IChatMessageCache>(new ChatMessageCache(100));
            services.AddHostedService<CacheInitializationService>();

            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();

            services.AddSingleton<ISentimentAnalysisProcessor, SentimentAnalysisProcessor>();
            services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();
            services.AddScoped<ISentimentAnalysisRepository, SentimentAnalysisRepository>();

            services.AddControllers();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

        }

        private static void Configure(WebApplication app)
        {
            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //app.UseHsts();
            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");

            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

        }

        public static void ConfigureDatabaseService(IServiceCollection services, IConfiguration configuration)
        {
            string sqlConnectionString = configuration["AzureSQLDatabase:ConnectionString"];
            if (string.IsNullOrEmpty(sqlConnectionString))
                throw new InvalidOperationException("AzureSQLDatabase:ConnectionString configuration is missing.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));
        }
        public static void ConfigureSignalIRService(IServiceCollection services, IConfiguration configuration)
        {
            string signalRConnectionString = configuration["AzureSignalRService:ConnectionString"];

            if (string.IsNullOrEmpty(signalRConnectionString))
                throw new InvalidOperationException("AzureSignalRService:ConnectionString configuration is missing.");

            services.AddSignalR().AddAzureSignalR(signalRConnectionString);
        }

        public static void ConfigureTextAnalyticsClient(IServiceCollection services, IConfiguration configuration)
        {
            string endpoint = configuration["AzureSentimentAnalysisService:Endpoint"];
            string apiKey = configuration["AzureSentimentAnalysisService:Key"];

            if(string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("AzureSentimentAnalysisService:Endpoint configuration is missing.");

            if (string.IsNullOrEmpty(endpoint))
                throw new InvalidOperationException("AzureSentimentAnalysisService:Key configuration is missing.");


            var credentials = new AzureKeyCredential(apiKey);

            services.AddSingleton(new TextAnalyticsClient(new Uri(endpoint), credentials));
        }


        private static void CheckDbConnection(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    if (!dbContext.Database.CanConnect())
                    {
                        throw new Exception("Cannot connect to the database.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to connect to database: {ex.Message}");
                }
            }
        }
    }
}
