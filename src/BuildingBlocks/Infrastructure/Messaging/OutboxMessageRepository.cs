using System;
using Domain.Core.Data;

namespace Infrastructure.Messaging
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly MessagingDataContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public OutboxMessageRepository(MessagingDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}