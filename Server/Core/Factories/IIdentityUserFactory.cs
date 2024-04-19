using Server.Common.Core.Abstract;
using Server.Core.Models;

namespace Server.Core.Factories;

public interface IIdentityUserFactory : IFactory<IdentityUser>
{
    IIdentityUserFactory WithEmail(string email);
    
    IIdentityUserFactory WithPassword(string password);
}