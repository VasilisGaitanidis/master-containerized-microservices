using System.Reflection;
using Application;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
        {
            services
                .AddApplication()
                .AddCqrsHandlers()
                .AddValidators()
                .AddMappings();

            return services;
        }

        public static IServiceCollection AddCqrsHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}