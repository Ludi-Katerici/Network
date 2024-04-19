using Microsoft.AspNetCore.Identity;
using Server.Common.Core.Abstract;
using IdentityUser=Server.Modules.Identity.Core.Models.IdentityUser;

namespace Server.Modules.Identity.Core.Services;

public interface IPasswordService : IPasswordHasher<IdentityUser>
{
    List<IException> ValidatePassword(string password);
}

