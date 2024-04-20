using FluentValidation;

namespace FrontEnd.Components.Pages;

public class CreateEventFormInputValidator : AbstractValidator<CreateEventFormInputModel>
{
    public CreateEventFormInputValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Моля въведете име на събитието");
        
        this.RuleFor(x => x.Region)
            .NotEmpty()
            .WithMessage("Моля въведете регион");
        
        this.RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Моля въведете град");
        
        this.RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Моля въведете адрес");
        
        this.RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("Моля изберете поне една категория");
        
        this.RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Моля въведете описание на събитието");
        
        this.RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Моля въведете начална дата и час на събитието")
            .Must(x => x > DateTime.Now)
            .WithMessage("Началната дата и час на събитието трябва да бъдат в бъдещето");
    }
}