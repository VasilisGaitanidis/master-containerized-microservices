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
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddUnitOfWork()
                .AddDomainEventDispatcher()
                .AddConsulServiceDiscovery(configuration);
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