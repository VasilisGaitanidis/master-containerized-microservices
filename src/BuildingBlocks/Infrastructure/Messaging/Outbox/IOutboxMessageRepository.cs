using System;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Infrastructure.Messaging.Outbox
{
    public interface IOutboxMessageRepository : IRepository<OutboxMessage, Guid>
    {
        Task<OutboxMessage> AddAsync(OutboxMessage message);
    }
}