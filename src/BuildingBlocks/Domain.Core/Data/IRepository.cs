using Domain.Core.Models;

namespace Domain.Core.Data
{
    /// <summary>
    /// The <typeparamref name="TEntity"/> repository pattern.
    /// </summary>
    /// <typeparam name="TEntity">The aggregate root entity.</typeparam>
    /// <typeparam name="TId">The aggregate root identifier.</typeparam>
    public interface IRepository<TEntity, TId> where TEntity : class, IAggregateRoot<TId>
    {
        /// <summary>
        /// The unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}