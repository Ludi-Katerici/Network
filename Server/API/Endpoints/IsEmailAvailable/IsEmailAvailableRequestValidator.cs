using Contracts.Endpoints.IsEmailAvailable;
using FastEndpoints;
using FluentValidation;

namespace Server.API.Endpoints.IsEmailAvailable;

internal sealed class IsEmailAvailableRequestValidator : Validator<IsEmailAvailableRequest>
{
    public IsEmailAvailableRequestValidator()
    {
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}