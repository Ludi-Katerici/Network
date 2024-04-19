namespace Server.Common.Core.Abstract;

public interface IFactory<out TEntity>
    where TEntity : IAggregateRoot
{
    TEntity Build();
}