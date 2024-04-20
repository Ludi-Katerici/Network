using System.Security.Claims;
using FastEndpoints.Security;

namespace Server.Common.API.Authorization;

public static class ExtensionMethods
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user
            .ClaimValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("UserId is not present in claims");

        return Guid.Parse(userId);
    }
}