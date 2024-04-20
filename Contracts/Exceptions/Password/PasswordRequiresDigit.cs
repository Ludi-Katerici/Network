namespace Contracts.Exceptions.Password;

public sealed class PasswordRequiresDigit : IException
{
    public static PasswordRequiresDigit Instance { get; } = new();
    public string ErrorMessage => "Паролата трябва да съдържа поне една цифра.";
    
    public static bool IsValid(string password) => password.Any(char.IsDigit);
}