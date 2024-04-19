using Microsoft.AspNetCore.Identity;
using Server.Common.Core.Abstract;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.Core.Services;

public interface IPasswordService : IPasswordHasher<IdentityUser>
{
    List<IException> ValidatePassword(string password);
}

