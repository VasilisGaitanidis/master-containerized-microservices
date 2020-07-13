namespace Domain.Core
{
    public interface IEntity<out TId> : IIdentity<TId>
    {
    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        /// <inheritdoc />
        public TId Id { get; }
    }
}