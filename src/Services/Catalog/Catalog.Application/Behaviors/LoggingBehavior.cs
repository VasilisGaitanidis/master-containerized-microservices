using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Behaviors
{
    /// <summary>
    /// Handles logging behavior for a <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">A generic request.</typeparam>
    /// <typeparam name="TResponse">A generic response.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="LoggingBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next();

            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            return response;
        }
    }
}