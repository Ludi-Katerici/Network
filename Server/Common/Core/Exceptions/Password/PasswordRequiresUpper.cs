using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Password;

public sealed class PasswordRequiresUpper : IException
{
    public string Discriminator => nameof(PasswordRequiresUpper);
    
    public static PasswordRequiresUpper Instance { get; } = new();
    public string ErrorMessage => "Паролата трябва да съдържа поне една главна буква.";
    
    public static bool IsValid(string password) => password.Any(char.IsUpper);
}