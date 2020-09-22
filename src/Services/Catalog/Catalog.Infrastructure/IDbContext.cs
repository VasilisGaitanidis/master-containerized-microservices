using System;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
    public interface IDbContext
    {
        /// <summary>
        /// Begins an asynchronous transaction.
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commits an asynchronous transaction.
        /// </summary>
        /// <returns></returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Rollbacks a transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Creates an execution strategy.
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task RetryOnExceptionAsync(Func<Task> operation);
    }
}