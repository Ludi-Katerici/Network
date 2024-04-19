using Server.Common.Core;
using Server.Modules.Identity.Core.Factories;
using Server.Modules.Identity.Core.Models;
using Server.Modules.Identity.Core.Services;

namespace Server.Modules.Identity.Infrastructure.Factories;

public sealed class IdentityUserFactory : IIdentityUserFactory
{
    private readonly IPasswordService passwordService;
    public IdentityUserFactory(IPasswordService passwordService) 
        => this.passwordService = passwordService;
    
    private readonly Step<string> emailStep = new();
    private readonly Step<string> passwordStep = new();


    public IdentityUser Build()
    {
        if ((this.emailStep.HasValue &&
             this.passwordStep.HasValue) is false)
        {
            throw new InvalidOperationException("Cannot build rejection without all properties");
        }
        
        var user = new IdentityUser(
            email: this.emailStep.Value);
        
        user.PasswordHash = this.passwordService.HashPassword(user, this.passwordStep.Value);

        return user;
    }
    public IIdentityUserFactory WithEmail(string email)
    {
        this.emailStep.SetValue(email);
        return this;
    }
    public IIdentityUserFactory WithPassword(string password)
    {
        this.passwordStep.SetValue(password);
        return this;
    }
}