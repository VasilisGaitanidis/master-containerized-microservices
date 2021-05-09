using System;
using Consul;
using Domain.Data;
using Infrastructure.Consul;
using Infrastructure.Data;
using Infrastructure.DomainEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// The building blocks infrastructure <see cref="IServiceCollection"/> extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Entity Framework Unit of Work on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddEntityFrameworkUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        }

        /// <summary>
        /// Add domain event dispatcher on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
        {
            return services
                .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        }

        /// <summary>
        /// Add Consul service discovery on <paramref name="services"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddConsulServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
        {
            var consulOptions = configuration.GetOptions<ConsulOptions>("Consul");

            services.AddSingleton(consulOptions);

            services.AddSingleton<IHostedService, ConsulServiceDiscoveryHostedService>();

            services.AddSingleton<IConsulClient, ConsulClient>(provider =>
                new ConsulClient(clientConfiguration =>
                    clientConfiguration.Address = new Uri(consulOptions.ConsulAddress)));

            return services;
        }
    }
}