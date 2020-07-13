using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}