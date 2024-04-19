using FastEndpoints;
using FluentValidation;
using Server.Modules.Identity.API.SDK.Endpoints.IsEmailAvailable;

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