using Contracts.Endpoints.DeleteUser;
using FastEndpoints;
using FluentValidation;

namespace Server.API.Endpoints.DeleteUser;

internal sealed class DeleteUserRequestValidator : Validator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        this.RuleFor(x => x.UserId)
            .NotEmpty();
    }
}