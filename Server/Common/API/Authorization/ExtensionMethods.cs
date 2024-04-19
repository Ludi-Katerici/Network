using System.Security.Claims;
using FastEndpoints.Security;
using Server.Common.API.Authorization.Claims;

namespace Server.Common.API.Authorization;

public static class ExtensionMethods
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user
            .ClaimValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("UserId is not present in claims");

        return Guid.Parse(userId);
    }
    
    public static Guid GetCompanyId(this ClaimsPrincipal user)
    {
        var values = CompanyClaim.GetValues(user.ClaimValue(CompanyClaim.TypeValue) ?? throw new InvalidOperationException("CompanyId is not present in claims"));

        return Guid.Parse(values.CompanyId);
    }
}