using RealtimeChatApp.Repository;
using RealtimeChatApp.Services.Cache;

public class CacheInitializationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IChatMessageCache _cache;
    private readonly ILogger<CacheInitializationService> _logger;

    public CacheInitializationService(
        IServiceProvider serviceProvider,
        IChatMessageCache cache,
        ILogger<CacheInitializationService> logger)
    {
        _serviceProvider = serviceProvider;
        _cache = cache;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting cache initialization...");

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IChatMessageRepository>();
            
            var messages = await repository.GetAsync(_cache.Capacity, 0, cancellationToken);
            
           _cache.Initialize(messages);

            _logger.LogInformation("Cache initialization completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during cache initialization");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
} 