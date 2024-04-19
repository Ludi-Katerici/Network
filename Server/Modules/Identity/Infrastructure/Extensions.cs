using Microsoft.AspNetCore.Identity;
using IdentityUser=Server.Modules.Identity.Core.Models.IdentityUser;

namespace Server.Modules.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        
        return services;
    }
}