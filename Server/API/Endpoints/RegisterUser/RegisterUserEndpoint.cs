using Contracts.Endpoints.RegisterUser;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Common.Core.Exceptions.Email;
using Server.Core.Factories;
using Server.Core.Models;
using Server.Core.Services;
using Server.Persistence;

namespace Server.API.Endpoints.RegisterUser;

internal sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest>
{
    public DataContext DataContext { get; set; }
    public IIdentityUserFactory IdentityUserFactory { get; set; }
    public IPasswordService PasswordService { get; set; }

    public override void Configure()
    {
        this.Post(RegisterUserRequest.Route);
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        this.ValidatePassword(req.Password);
        await this.ValidateEmail(req.Email);
        this.ThrowIfAnyErrors();

        var user = await this.CreateUser(req, ct);

        await this.SendOkAsync(
            new RegisterUserResponse
            {
                Id = user.Id,
                Email = user.Email,
            });
    }
    private async Task<IdentityUser> CreateUser(RegisterUserRequest req, CancellationToken ct)
    {
        var factory = this.IdentityUserFactory
            .WithEmail(req.Email)
            .WithPassword(req.Password);

        var user = factory.Build();

        await this.DataContext.AddAsync(user, ct);
        await this.DataContext.SaveChangesAsync(ct);

        return user;
    }

    private void ValidatePassword(string password)
    {
        var errors = this.PasswordService.ValidatePassword(password);

        foreach (var error in errors)
        {
            this.AddError(x => x.Password, error.Discriminator);
        }
    }

    private async Task ValidateEmail(string email)
    {
        var emailToUpperCase = email.ToUpper();
        var isEmailTaken = await this.DataContext.Users.AsNoTracking().AnyAsync(x => x.Email == emailToUpperCase);
        
        if (isEmailTaken)
        {
            this.AddError(x => x.Email, EmailMustBeUnique.Instance.Discriminator);
        }
    }
}