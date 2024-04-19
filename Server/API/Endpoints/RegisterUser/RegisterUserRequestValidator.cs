using FastEndpoints;
using FluentValidation;
using Server.API.SDK.Endpoints.RegisterUser;

namespace Server.API.Endpoints.RegisterUser;

internal sealed class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        this.RuleFor(x => x.Password)
            .NotEmpty();
    }
}