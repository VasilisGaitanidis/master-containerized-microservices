﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}