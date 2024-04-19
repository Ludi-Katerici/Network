using Contracts.Endpoints.RegisterUser;
using FastEndpoints;
using FluentValidation;

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