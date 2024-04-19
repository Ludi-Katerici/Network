using FastEndpoints;
using FluentValidation;
using Server.API.SDK.Endpoints.DeleteUser;

namespace Server.API.Endpoints.DeleteUser;

internal sealed class DeleteUserRequestValidator : Validator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        this.RuleFor(x => x.UserId)
            .NotEmpty();
    }
}