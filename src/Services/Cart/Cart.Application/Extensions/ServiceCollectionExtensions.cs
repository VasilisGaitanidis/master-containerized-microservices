using System.Reflection;
using AutoMapper;
using Cart.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Application.Extensions
{
    /// <summary>
    /// The cart application <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add cart application on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCartApplication(this IServiceCollection services)
        {
            return services
                    .AddCommandAndQueryHandlers()
                    .AddPipelineBehaviors()
                    .AddValidators()
                    .AddMappings();
        }

        /// <summary>
        /// Add MediatR command and query handlers on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddCommandAndQueryHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Add MediatR pipeline behaviors on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        }

        /// <summary>
        /// Add FluentValidation validators on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Add AutoMapper profiles on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}