using Contracts.Endpoints.GetEventDetails;
using FluentValidation;

namespace Server.API.Endpoints.GetEventDetails;

public class GetEventDetailsValidator : AbstractValidator<GetEventDetailsRequest>
{
    public GetEventDetailsValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Грешка при валидация на идентификатора на събитието.");
    }
}