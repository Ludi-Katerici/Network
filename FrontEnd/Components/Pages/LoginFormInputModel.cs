using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace FrontEnd.Components.Pages;

public sealed class LoginFormInputModel
{
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}

public class LoginFormInputModelValidator : AbstractValidator<LoginFormInputModel>
{
    public LoginFormInputModelValidator()
    {
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Моля, въведете имейл адрес.")
            .EmailAddress()
            .WithMessage("Имейд адресът е невалиден.");

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Моля, въведете парола.");
    }
}