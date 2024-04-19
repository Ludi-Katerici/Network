using System.Security.Claims;
using Contracts.Endpoints.LoginUser;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.API.Endpoints.LoginUser;

internal sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, LoginUserResponse>
{
    public DataContext DataContext { get; set; }
    public PasswordHasher<IdentityUser> PasswordHasher { get; set; }

    public override void Configure()
    {
        this.Post(LoginUserRequest.Route);
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var user = await this.GetUserByEmail(req.Email, ct);

        if (user is null)
        {
            await this.SendErrorsAsync(cancellation: ct);
            return;
        }

        var result = this.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            await this.SendErrorsAsync(cancellation: ct);
            return;
        }

        await CookieAuth.SignInAsync(cfg => {
            cfg.Roles.AddRange(user.AuthorizationDetails.Roles.Select(x => x.Discriminator));
            cfg.Claims.AddRange(user.AuthorizationDetails.Claims.Select(x => new Claim(x.Type, x.Value)));
            cfg.Permissions.AddRange(user.AuthorizationDetails.Permissions.Select(x => x.Discriminator));

            cfg.Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            cfg.Claims.Add(new Claim(ClaimTypes.Email, user.Email));
        });

        await this.SendOkAsync(
            new LoginUserResponse
            {
                Claims = user.AuthorizationDetails.Claims.Select(x => x.Type).ToList(),
                Roles = user.AuthorizationDetails.Roles.Select(x => x.Discriminator).ToList(),
                Permissions = user.AuthorizationDetails.Permissions.Select(x => x.Discriminator).ToList(),
                UserId = user.Id,
            });
    }

    private async Task<IdentityUser?> GetUserByEmail(string email, CancellationToken ct = default)
    {
        var emailToUpperCase = email.ToUpper();

        return await this.DataContext.Users
            .AsNoTracking()
            .Include(x => x.AuthorizationDetails)
            .FirstOrDefaultAsync(x => x.Email == emailToUpperCase, ct);
    }

}