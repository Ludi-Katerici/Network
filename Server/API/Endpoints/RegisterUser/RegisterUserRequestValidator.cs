using Contracts;
using Contracts.Endpoints.RegisterUser;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Server.Common.Core.Exceptions.Email;
using Server.Common.Core.Exceptions.Password;
using Server.Common.Core.Exceptions.PhoneNumber;
using Server.Persistence;

namespace Server.API.Endpoints.RegisterUser;

internal sealed class RegisterUserRequestValidator : Validator<RegisterUserRequest>
{
    public RegisterUserRequestValidator(DataContext dataContext)
    {
        this.RuleFor(x => x.Name)
            .NotEmpty();
        
        this.RuleFor(x => x.Family)
            .NotEmpty();
        
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, token) => {
                var emailToUpperCase = email.ToUpper();
                var isEmailTaken = await dataContext.Users.AnyAsync(x => x.Email == emailToUpperCase, token);
                
                return !isEmailTaken;
            })
            .WithMessage(EmailMustBeUnique.Instance.ErrorMessage);
        
        this.RuleFor(x => x.PhoneNumber)
            .Must(phoneNumber => {
                if (phoneNumber.Contains("+359"))
                {
                    return phoneNumber.Length == 13;
                }
                
                return phoneNumber.Length == 10;
            })
            .WithMessage(PhoneNumberMustBeUnique.Instance.ErrorMessage)
            .NotEmpty();

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
            .NotEmpty();
        
        this.RuleFor(x => x.City)
            .Must((dto, city) => {
                return !RegionsService.RegionsList.Any(x => x.RegionName == dto.Region && x.Cities.Contains(city));
            })
            .WithMessage("Не съществува такъв град в тази област.")
            .NotEmpty();
        
        this.RuleFor(x => x.ProfessionalExperience)
            .NotEmpty();
        
        this.RuleFor(x => x.Interests)
            .NotEmpty();
        
        this.RuleFor(x => x.Searchings)
            .NotEmpty();
        
        this.RuleFor(x => x.AdditionalInformation)
            .NotEmpty();
    }
}