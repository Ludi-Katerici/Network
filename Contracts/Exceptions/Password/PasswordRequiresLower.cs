namespace Contracts.Exceptions.Password;

public sealed class PasswordRequiresLower : IException
{
    public string Discriminator => nameof(PasswordRequiresLower);
    
    public static PasswordRequiresLower Instance { get; } = new();
    public string ErrorMessage => "Паролата трябва да съдържа поне една малка буква.";
    
    public static bool IsValid(string password) => password.Any(char.IsLower);
}
