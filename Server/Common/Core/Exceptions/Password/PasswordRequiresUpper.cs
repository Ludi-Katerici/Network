using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Password;

public sealed class PasswordRequiresUpper : IException
{
    public string Discriminator => nameof(PasswordRequiresUpper);
    
    public static PasswordRequiresUpper Instance { get; } = new();
}