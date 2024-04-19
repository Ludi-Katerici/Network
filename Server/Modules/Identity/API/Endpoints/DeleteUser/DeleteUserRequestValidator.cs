using FastEndpoints;
using FluentValidation;
using Identity.API.SDK.Endpoints.DeleteUser;

namespace Identity.API.Endpoints.DeleteUser;

internal sealed class DeleteUserRequestValidator : Validator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        this.RuleFor(x => x.UserId)
            .NotEmpty();
    }
}