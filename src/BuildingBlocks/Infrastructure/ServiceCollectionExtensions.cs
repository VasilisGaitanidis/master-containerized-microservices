using Domain.Core.Data;
using Infrastructure.Data;
using Infrastructure.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services
                .AddUnitOfWork()
                .AddDomainEventDispatcher();
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