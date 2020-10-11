using Domain.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Messaging.Outbox
{
    public class MessagingDataContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// The default database schema.
        /// </summary>
        public const string DefaultSchema = "messaging";

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public MessagingDataContext(DbContextOptions<MessagingDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
    }
}