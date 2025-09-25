namespace Meteron.Domain.Common;

public abstract class EntityBase<TId>(TId id) where TId : struct
{
    public TId Id { get; protected set; } = id;
}