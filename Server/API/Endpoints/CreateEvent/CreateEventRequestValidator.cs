using Contracts;
using Contracts.Endpoints.CreateEvent;
using FluentValidation;
using Server.Common.Core.Abstract;

namespace Server.API.Endpoints.CreateEvent;

public sealed class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventRequestValidator(IClock clock)
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Моля въведете име на събитието.");
        
        this.RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Моля въведете описание на събитието.");
        
        this.RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Моля въведете начална дата на събитието.")
            .Must(x => x > clock.CurrentDateTime())
            .WithMessage("Началната дата на събитието трябва да бъде в бъдещето.");

        this.RuleFor(x => x.Region)
            .NotEmpty()
            .WithMessage("Моля въведете регион на събитието.");
        
        this.RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Моля въведете адрес на събитието.");
        
        this.RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("Моля въведете категории на събитието.");
    }
}