using FastEndpoints;
using FluentValidation;
using Server.API.SDK.Endpoints.IsEmailAvailable;

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