using Server.Core.Factories;
using Server.Core.Services;
using Server.Infrastructure.Factories;
using Server.Infrastructure.Services;

namespace Server.API;

public static class ApiExtensions
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IIdentityUserFactory, IdentityUserFactory>();

        return services;
    }
}