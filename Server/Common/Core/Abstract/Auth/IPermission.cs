namespace Server.Common.Core.Abstract.Auth;

public interface IPermission
{
    string Discriminator { get; }
}