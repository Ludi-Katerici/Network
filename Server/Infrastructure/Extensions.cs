using Microsoft.AspNetCore.Identity;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        
        return services;
    }
}