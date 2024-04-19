using Server.Common.Core.Abstract;

namespace Server.Common.Core;

public class Entity<TId> : IEntity
    where TId : struct
{
    protected Entity(TId id) => this.Id = id;
    protected Entity() {}
    
    public TId Id { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }
        
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        if (this.Id.Equals(default) || other.Id.Equals(default))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    public override int GetHashCode() => (this.GetType().ToString() + this.Id).GetHashCode();
}