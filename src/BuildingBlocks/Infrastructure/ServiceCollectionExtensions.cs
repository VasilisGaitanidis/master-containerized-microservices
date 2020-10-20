using Domain.Core.Data;
using Infrastructure.Behaviors;
using Infrastructure.Data;
using Infrastructure.DomainEvents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddPipelineBehaviors()
                .AddUnitOfWork()
                .AddDomainEventDispatcher();
        }

        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
        {
            return services
                .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}