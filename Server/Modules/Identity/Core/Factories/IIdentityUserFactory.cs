using Server.Common.Core.Abstract;
using Server.Modules.Identity.Core.Models;

namespace Server.Modules.Identity.Core.Factories;

public interface IIdentityUserFactory : IFactory<IdentityUser>
{
    IIdentityUserFactory WithEmail(string email);
    
    IIdentityUserFactory WithPassword(string password);
}