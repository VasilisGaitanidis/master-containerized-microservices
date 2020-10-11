using System;
using System.Threading.Tasks;
using Domain.Core.Data;

namespace Infrastructure.Messaging.Outbox
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly MessagingDataContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public OutboxMessageRepository(MessagingDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<OutboxMessage> AddAsync(OutboxMessage message)
        {
            var entityEntry = await _context.OutboxMessages.AddAsync(message);

            return entityEntry.Entity;
        }
    }
}