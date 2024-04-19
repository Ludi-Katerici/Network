using Microsoft.EntityFrameworkCore;

namespace Server.Common.Infrastructure;

public sealed class DbContextAppInitializer : IHostedService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger<DbContextAppInitializer> logger;

    public DbContextAppInitializer(IServiceScopeFactory serviceScopeFactory, ILogger<DbContextAppInitializer> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
    }
        
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        await using var scope = this.serviceScopeFactory.CreateAsyncScope();
        foreach (var dbContextType in dbContextTypes)
        {
            if (scope.ServiceProvider.GetService(dbContextType) is not DbContext dbContext)
            {
                continue;
            }
                
            this.logger.LogInformation("Running DB context: {Name}...", dbContext.GetType().Name);
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}