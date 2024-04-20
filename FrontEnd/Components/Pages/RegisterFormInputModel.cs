using Contracts;
using Contracts.Exceptions.Email;
using Contracts.Exceptions.Password;
using Contracts.Exceptions.PhoneNumber;
using FluentValidation;

namespace FrontEnd.Components.Pages;

public sealed class RegisterFormInputModel
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string Work { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    public string ProfessionalExperience { get; set; } = string.Empty;
    public string Interests { get; set; } = string.Empty;
    public string Searchings { get; set; } = string.Empty;
    public string AdditionalInformation { get; set; } = string.Empty;
}

public class RegisterFormInputModelValidator : AbstractValidator<RegisterFormInputModel>
{
    public RegisterFormInputModelValidator(IIdentityApiService identityApiService)
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Моля, въведете име.");
        
        this.RuleFor(x => x.Family)
            .NotEmpty()
            .WithMessage("Моля, въведете фамилия.");

        this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Моля, въведете имейл адрес.")
            .EmailAddress()
            .WithMessage("Имейл адресът е невалиден.")
            .MustAsync(async (x, _) => {
                if (string.IsNullOrWhiteSpace(x))
                    return true;

                var result = await identityApiService.IsEmailAvailable(x);
                
                return result.Content;

            })
            .WithMessage("Този имейл адрес вече се използва.");

        this.RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Моля, въведете телефонен номер.")
            .Must(phoneNumber => {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                    return true;
                
                if (phoneNumber.Contains("+359"))
                {
                    return phoneNumber.Length == 13;
                }

                return phoneNumber.Length == 10;
            })
            .WithMessage("Телефонния номер е невалиден.");
            

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Моля, въведете парола.")
            .Must(PasswordRequiresDigit.IsValid)
            .WithMessage(PasswordRequiresDigit.Instance.ErrorMessage)
            .Must(PasswordRequiresLower.IsValid)
            .WithMessage(PasswordRequiresLower.Instance.ErrorMessage)
            .Must(PasswordRequiresUpper.IsValid)
            .WithMessage(PasswordRequiresUpper.Instance.ErrorMessage)
            .Must(PasswordTooShort.IsValid)
            .WithMessage(PasswordTooShort.Instance.ErrorMessage);
        
        this.RuleFor(x => x.ProfessionalExperience)
            .NotEmpty()
            .WithMessage("Моля, въведете професионален опит.");
        
        this.RuleFor(x => x.Interests)
            .NotEmpty()
            .WithMessage("Моля, въведете интереси.");
        
        this.RuleFor(x => x.Searchings)
            .NotEmpty()
            .WithMessage("Моля, въведете търсения.");
        
        this.RuleFor(x => x.AdditionalInformation)
            .NotEmpty()
            .WithMessage("Моля, въведете допълнителна информация.");
        
        this.RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Моля, потвърдете паролата.")
            .Equal(x => x.Password)
            .WithMessage("Паролите не съвпадат.");
        
        this.RuleFor(x => x.Work)
            .NotEmpty()
            .WithMessage("Моля, въведете работа.");
        
        this.RuleFor(x => x.Education)
            .NotEmpty()
            .WithMessage("Моля, въведете образование.");
    }
}