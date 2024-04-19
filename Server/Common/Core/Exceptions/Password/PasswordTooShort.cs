using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Password;

public sealed class PasswordTooShort : IException
{
    public string Discriminator => nameof(PasswordTooShort);
    
    public static PasswordTooShort Instance { get; } = new();
}