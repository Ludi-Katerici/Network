using Server.Common.Core.Abstract;

namespace Server.Common.Core.Exceptions.Email;

public sealed class EmailMustBeUnique : IException
{
    public string Discriminator => nameof(EmailMustBeUnique);
    
    public static EmailMustBeUnique Instance { get; } = new();
}