using System;

namespace Infrastructure.Bus.Middlewares
{
    public interface IMessage
    {
        Guid Id { get; }

        Guid CorrelationId { get; }

        DateTime? CreatedDate { get; }
    }
}