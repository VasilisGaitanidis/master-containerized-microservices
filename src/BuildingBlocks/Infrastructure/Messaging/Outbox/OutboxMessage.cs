using System;
using Domain.Models;

namespace Infrastructure.Messaging.Outbox
{
    public class OutboxMessage : AggregateRoot<Guid>
    {
        /// <summary>
        /// The date the message occurred.
        /// </summary>
        public DateTime OccurredOn { get; }

        /// <summary>
        /// The event type full name.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// The event data - serialized to JSON.
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// The date the message processed.
        /// </summary>
        public DateTime? ProcessedOn { get; private set; }

        /// <summary>
        /// Initializes a new outbox message.
        /// </summary>
        /// <param name="id">The outbox message identifier.</param>
        /// <param name="occurredOn">The outbox message date occurred on.</param>
        /// <param name="type">The outbox message type.</param>
        /// <param name="data">The outbox message data.</param>
        public OutboxMessage(Guid id, DateTime occurredOn, string type, string data)
            : base(id)
        {
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }

        /// <summary>
        /// Sets outbox message process date.
        /// </summary>
        public void ChangeProcessDate()
        {
            ProcessedOn = DateTime.UtcNow;
        }
    }
}