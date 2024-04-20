using Microsoft.EntityFrameworkCore;
using Server.Common.Core.Abstract;

namespace Server.Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    private const string SectionName = "SqlServer";
    
    public static IServiceCollection AddCommonModule(this IServiceCollection services)
    {
        services.AddHostedService<DbContextAppInitializer>();
        services.AddTransient<IClock, Clock>();
        
        return services;
    }
    
    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services) 
        where T : DbContext
        => services.AddDbContext<T>((sp, optionsBuilder) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration[$"{SectionName}:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString, o => o.MigrationsHistoryTable($"{typeof(T).Name}_MigrationsHistory"));
        });

    public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration)
        where TConfig : class, new()
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var config = new TConfig();
        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }

    public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration,
        Func<TConfig> pocoProvider) where TConfig : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(pocoProvider);

        var config = pocoProvider();
        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }

    public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration,
        TConfig config) where TConfig : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(config);

        configuration.Bind(config);
        services.AddSingleton(config);
        return config;
    }
}