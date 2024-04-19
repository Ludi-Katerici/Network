namespace Server.Common.Core.Abstract.Auth;

public interface IRole
{
    string Discriminator { get; }
}