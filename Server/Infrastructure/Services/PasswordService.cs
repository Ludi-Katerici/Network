using Microsoft.AspNetCore.Identity;
using Server.Common.Core.Abstract;
using Server.Common.Core.Exceptions.Password;
using Server.Core.Services;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.Infrastructure.Services;

public sealed class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<IdentityUser> passwordHasher;
    public PasswordService(IPasswordHasher<IdentityUser> passwordHasher) 
        => this.passwordHasher = passwordHasher;

    public string HashPassword(IdentityUser user, string password) => this.passwordHasher.HashPassword(user, password);
    public PasswordVerificationResult VerifyHashedPassword(IdentityUser user, string hashedPassword, string providedPassword) 
        => this.passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    public List<IException> ValidatePassword(string password)
    {
        var exceptions = new List<IException>();

        if (password.Length < 8)
        {
            exceptions.Add(PasswordTooShort.Instance);
        }

        if (password.Any(char.IsUpper) is false)
        {
            exceptions.Add(PasswordRequiresUpper.Instance);
        }
        
        if (password.Any(char.IsLower) is false)
        {
            exceptions.Add(PasswordRequiresLower.Instance);
        }
        
        if (password.Any(char.IsDigit) is false)
        {
            exceptions.Add(PasswordRequiresDigit.Instance);
        }
        
        return exceptions;
    }
    
}