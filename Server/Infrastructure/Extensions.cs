using Microsoft.AspNetCore.Identity;
using Server.Common.Infrastructure;
using Server.Persistence;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddSqlServer<DataContext>();
        services.AddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
        
        return services;
    }
}