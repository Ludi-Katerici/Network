using Contracts;
using Contracts.Endpoints.RegisterUser;
using Contracts.Exceptions.Email;
using Contracts.Exceptions.Password;
using Contracts.Exceptions.PhoneNumber;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.RegisterUser;

internal sealed class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        this.RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Моля въведете име.");
        
        this.RuleFor(x => x.Family)
            .NotEmpty()
            .WithMessage("Моля въведете фамилия.");
        
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Моля въведете имейл.")
            .EmailAddress()
            .WithMessage("Моля въведете валиден имейл.")
            .MustAsync(async (email, token) => {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return true;
                }

                var dataContext = Resolve<DataContext>();
                var emailToUpperCase = email.ToUpper();
                var isEmailTaken = await dataContext.Users.AnyAsync(x => x.Email == emailToUpperCase, token);
                
                return !isEmailTaken;
            })
            .WithMessage(EmailMustBeUnique.Instance.ErrorMessage);

        this.RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Моля въведете телефонен номер.")
            .Must(phoneNumber => {
                if (phoneNumber.Contains("+359"))
                {
                    return phoneNumber.Length == 13;
                }

                return phoneNumber.Length == 10;
            })
            .WithMessage(PhoneNumberMustBeUnique.Instance.ErrorMessage);
            

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .Must(PasswordRequiresDigit.IsValid)
            .WithMessage(PasswordRequiresDigit.Instance.ErrorMessage)
            .Must(PasswordRequiresLower.IsValid)
            .WithMessage(PasswordRequiresLower.Instance.ErrorMessage)
            .Must(PasswordRequiresUpper.IsValid)
            .WithMessage(PasswordRequiresUpper.Instance.ErrorMessage)
            .Must(PasswordTooShort.IsValid)
            .WithMessage(PasswordTooShort.Instance.ErrorMessage);
        
        this.RuleFor(x => x.Region)
            .NotEmpty()
            .WithMessage("Моля въведете област.");

        this.RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Моля въведете град.")
            .Must((dto, city) => {
                return !RegionsService.Regions.Any(x => x.RegionName == dto.Region && x.Cities.Contains(city));
            })
            .WithMessage("Не съществува такъв град в тази област.");
            
        
        this.RuleFor(x => x.ProfessionalExperience)
            .NotEmpty()
            .WithMessage("Моля въведете професионален опит.");
        
        this.RuleFor(x => x.Interests)
            .NotEmpty()
            .WithMessage("Моля въведете интереси.");
        
        this.RuleFor(x => x.Searchings)
            .NotEmpty()
            .WithMessage("Моля въведете търсения.");
        
        this.RuleFor(x => x.AdditionalInformation)
            .NotEmpty()
            .WithMessage("Моля въведете допълнителна информация.");
    }
}