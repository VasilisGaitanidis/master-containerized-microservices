using System;
using Domain.Core.Data;

namespace Infrastructure.Messaging
{
    public interface IOutboxMessageRepository : IRepository<OutboxMessage, Guid>
    {
    }
}