using Server.Modules.Identity.Core.Factories;
using Server.Modules.Identity.Core.Services;
using Server.Modules.Identity.Infrastructure;
using Server.Modules.Identity.Infrastructure.Factories;
using Server.Modules.Identity.Infrastructure.Services;

namespace Server.Modules.Identity.API;

public static class ModuleExtensions
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IIdentityUserFactory, IdentityUserFactory>();

        services.AddInfrastructureLayer();
        
        return services;
    }
}