namespace Contracts.Exceptions.PhoneNumber;

public sealed class PhoneNumberMustBeUnique : IException
{
    public static PhoneNumberMustBeUnique Instance { get; } = new();
    public string ErrorMessage => "Този телефонен номер вече е регистриран.";
}