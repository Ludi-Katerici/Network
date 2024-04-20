namespace Contracts.Exceptions.Password;

public sealed class PasswordTooShort : IException
{
    public string Discriminator => nameof(PasswordTooShort);
    
    public static PasswordTooShort Instance { get; } = new();
    public string ErrorMessage => "Паролата трябва да бъде поне 6 символа дълга.";

    public static bool IsValid(string password) => password.Length > 6;
}