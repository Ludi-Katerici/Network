namespace Server.Common.Core.Abstract;

public interface IException
{
    string Discriminator { get; }
}