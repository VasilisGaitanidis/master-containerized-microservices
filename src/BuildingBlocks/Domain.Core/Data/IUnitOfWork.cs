using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    /// <summary>
    /// The unit of work pattern.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}