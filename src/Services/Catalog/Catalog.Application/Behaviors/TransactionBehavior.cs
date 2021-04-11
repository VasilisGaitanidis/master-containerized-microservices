using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Behaviors
{
    /// <summary>
    /// Handles transaction behavior for a <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">A generic request.</typeparam>
    /// <typeparam name="TResponse">A generic response.</typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of <see cref="TransactionBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;

            try
            {
                await _unitOfWork.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                    await _unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                    response = await next();

                    await _unitOfWork.CommitTransactionAsync();

                    _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}");

                _unitOfWork.RollbackTransaction();

                _logger.LogError(e.Message, e.StackTrace);

                throw;
            }
        }
    }
}