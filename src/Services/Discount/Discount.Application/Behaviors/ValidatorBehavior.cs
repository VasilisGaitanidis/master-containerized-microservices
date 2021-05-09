using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Behaviors
{
    /// <summary>
    /// Handles validation behavior for a <typeparamref name="TRequest"/>.
    /// </summary>
    /// <typeparam name="TRequest">A generic request.</typeparam>
    /// <typeparam name="TResponse">A generic response.</typeparam>
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidatorBehavior{TRequest,TResponse}"/>.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="validators">The validators.</param>
        public ValidatorBehavior(ILogger<ValidatorBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Validating {typeof(TRequest).Name}");

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!failures.Any())
            {
                return await next();
            }

            _logger.LogWarning($"Validation errors on {typeof(TRequest).Name}");

            throw new ValidationAppException($"Command validation errors for type {typeof(TRequest).Name}", new ValidationException(failures));
        }
    }
}