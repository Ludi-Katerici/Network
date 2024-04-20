using Contracts.Endpoints.RegisterUser;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Server.Persistence;
using IdentityUser=Server.Core.Models.IdentityUser;

namespace Server.API.Endpoints.RegisterUser;

internal sealed class RegisterUserEndpoint : Endpoint<RegisterUserRequest>
{
    public DataContext DataContext { get; set; }
    public IPasswordHasher<IdentityUser> PasswordHasher { get; set; }
    public override void Configure()
    {
        this.DontThrowIfValidationFails();
        this.DontCatchExceptions();
        this.Post(RegisterUserRequest.Route);
        this.AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
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
        var user = new IdentityUser(
            name: req.Name,
            family: req.Family,
            email: req.Email,
            phoneNumber: req.PhoneNumber,
            city: req.City,
            region: req.Region,
            professionalExperience: req.ProfessionalExperience,
            interests: req.Interests,
            searchings: req.Searchings,
            additionalInformation: req.AdditionalInformation);
        
        var passwordHash = this.PasswordHasher.HashPassword(user, req.Password);
        user.PasswordHash = passwordHash;

        await this.DataContext.AddAsync(user, ct);
        await this.DataContext.SaveChangesAsync(ct);

        return user;
    }
}