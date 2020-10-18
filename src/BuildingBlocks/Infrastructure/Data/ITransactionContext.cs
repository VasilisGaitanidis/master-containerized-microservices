﻿using System;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// Provides transactional behavior.
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// Begins an asynchronous transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        Task BeginTransactionAsync(IsolationLevel isolationLevel);

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