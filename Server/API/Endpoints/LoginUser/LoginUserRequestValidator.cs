using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace Server.API.Endpoints.LoginUser;

internal sealed class LoginUserRequestValidator : Validator<LoginRequest>
{
    public LoginUserRequestValidator()
    {
        this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        
        this.RuleFor(x => x.Password).NotEmpty();
    }
}